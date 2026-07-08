using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Aurora.SupplyWok.Platform.Shared.Application.Model;
using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;
using Aurora.SupplyWok.Platform.Subscriptions.Application.CommandServices;
using Aurora.SupplyWok.Platform.Subscriptions.Application.Internal.OutboundServices;
using Aurora.SupplyWok.Platform.Subscriptions.Domain.Model;
using Aurora.SupplyWok.Platform.Subscriptions.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Subscriptions.Domain.Repositories;
using Aurora.SupplyWok.Platform.Subscriptions.Infrastructure.Stripe.Configuration;
using Microsoft.Extensions.Options;

namespace Aurora.SupplyWok.Platform.Subscriptions.Application.Internal.CommandServices;

public class SubscriptionWebhookService(
    ISubscriptionRegistrationCommandService subscriptionRegistrationCommandService,
    IProcessedWebhookEventRepository processedWebhookEventRepository,
    IPendingRegistrationRepository pendingRegistrationRepository,
    IStripeCheckoutService stripeCheckoutService,
    IOptions<StripeSettings> stripeOptions,
    IUnitOfWork unitOfWork) : ISubscriptionWebhookService
{
    private const int TimestampToleranceSeconds = 300;
    private readonly StripeSettings _stripeSettings = stripeOptions.Value;

    public async Task<Result> ProcessStripeWebhookAsync(string signatureHeader, string payload,
        CancellationToken cancellationToken)
    {
        if (!IsValidSignature(signatureHeader, payload))
            return Result.Failure(SubscriptionsError.WebhookSignatureInvalid,
                nameof(SubscriptionsError.WebhookSignatureInvalid));

        try
        {
            using var document = JsonDocument.Parse(payload);
            var root = document.RootElement;
            var eventId = root.GetProperty("id").GetString();
            var eventType = root.GetProperty("type").GetString();

            if (string.IsNullOrWhiteSpace(eventId) || string.IsNullOrWhiteSpace(eventType))
                return Result.Failure(SubscriptionsError.WebhookPayloadInvalid,
                    nameof(SubscriptionsError.WebhookPayloadInvalid));

            if (await processedWebhookEventRepository.ExistsByStripeEventIdAsync(eventId, cancellationToken))
                return Result.Success();

            var stripeObject = root.GetProperty("data").GetProperty("object");

            return eventType switch
            {
                "checkout.session.completed" => await HandleCheckoutSessionCompletedAsync(eventId, stripeObject,
                    cancellationToken),
                "invoice.paid" or "invoice.payment_succeeded" or "invoice_payment.paid"
                    => await HandleInvoicePaidAsync(eventId, stripeObject, cancellationToken),
                "customer.subscription.updated" => await HandleSubscriptionSyncAsync(eventId, stripeObject,
                    cancellationToken),
                "customer.subscription.deleted" => await HandleSubscriptionSyncAsync(eventId, stripeObject,
                    cancellationToken),
                _ => Result.Success()
            };
        }
        catch (JsonException)
        {
            return Result.Failure(SubscriptionsError.WebhookPayloadInvalid,
                nameof(SubscriptionsError.WebhookPayloadInvalid));
        }
        catch (Exception ex)
        {
            return Result.Failure(SubscriptionsError.WebhookProcessingFailed, ex.Message);
        }
    }

    private async Task<Result> HandleCheckoutSessionCompletedAsync(string eventId, JsonElement stripeObject,
        CancellationToken cancellationToken)
    {
        var metadata = stripeObject.TryGetProperty("metadata", out var metadataElement) ? metadataElement : default;
        var publicIdRaw = metadata.ValueKind != JsonValueKind.Undefined &&
                          metadata.TryGetProperty("pending_registration_public_id", out var publicIdElement)
            ? publicIdElement.GetString()
            : null;

        if (!Guid.TryParse(publicIdRaw, out var publicId))
            return Result.Failure(SubscriptionsError.WebhookPayloadInvalid,
                "Stripe checkout session metadata is missing the pending registration identifier.");

        var pendingRegistration =
            await pendingRegistrationRepository.FindByPublicIdAsync(publicId, cancellationToken);
        if (pendingRegistration is null)
            return Result.Failure(SubscriptionsError.RegistrationNotFound,
                nameof(SubscriptionsError.RegistrationNotFound));

        var stripeCustomerId = stripeObject.TryGetProperty("customer", out var customerElement)
            ? customerElement.GetString()
            : null;
        var stripeSubscriptionId = stripeObject.TryGetProperty("subscription", out var subscriptionElement)
            ? subscriptionElement.GetString()
            : null;

        if (pendingRegistration.Status != Domain.Model.ValueObjects.EPendingRegistrationStatus.Provisioned)
            pendingRegistration.MarkCheckoutCompleted(stripeCustomerId, stripeSubscriptionId);
        else
            pendingRegistration.UpdateStripeReferences(stripeCustomerId, stripeSubscriptionId);

        await processedWebhookEventRepository.AddAsync(new Domain.Model.Aggregates.ProcessedWebhookEvent(eventId,
            "checkout.session.completed"), cancellationToken);
        try
        {
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(SubscriptionsError.WebhookProcessingFailed, ex.Message);
        }
    }

    private async Task<Result> HandleInvoicePaidAsync(string eventId, JsonElement stripeObject,
        CancellationToken cancellationToken)
    {
        var stripeSubscriptionId = TryGetInvoiceSubscriptionId(stripeObject);
        var stripeCustomerId = stripeObject.TryGetProperty("customer", out var customerElement)
            ? customerElement.GetString()
            : null;

        if (string.IsNullOrWhiteSpace(stripeSubscriptionId) || string.IsNullOrWhiteSpace(stripeCustomerId))
            return Result.Failure(SubscriptionsError.WebhookPayloadInvalid,
                "Invoice payload does not include Stripe subscription references.");

        var pendingRegistrationPublicId = TryGetInvoicePendingRegistrationPublicId(stripeObject);
        var details = await stripeCheckoutService.GetSubscriptionDetailsAsync(stripeSubscriptionId, cancellationToken);
        if (string.IsNullOrWhiteSpace(pendingRegistrationPublicId))
            pendingRegistrationPublicId = details.PendingRegistrationPublicId;

        if (!details.IsSuccess || string.IsNullOrWhiteSpace(pendingRegistrationPublicId) ||
            string.IsNullOrWhiteSpace(details.StripePriceId))
            return Result.Failure(SubscriptionsError.WebhookProcessingFailed,
                "Stripe subscription metadata could not be resolved for provisioning.");

        if (!Guid.TryParse(pendingRegistrationPublicId, out var publicId))
            return Result.Failure(SubscriptionsError.WebhookPayloadInvalid,
                "Stripe subscription metadata is missing the pending registration identifier.");

        var command = new ProvisionSubscriptionRegistrationCommand(
            eventId,
            publicId,
            stripeCustomerId,
            stripeSubscriptionId,
            details.StripePriceId,
            details.CurrentPeriodStart,
            details.CurrentPeriodEnd,
            details.Status ?? "incomplete");

        return await subscriptionRegistrationCommandService.Handle(command, cancellationToken);
    }

    private static string? TryGetInvoiceSubscriptionId(JsonElement invoiceObject)
    {
        if (invoiceObject.TryGetProperty("subscription", out var subscriptionElement))
            return subscriptionElement.GetString();

        if (!invoiceObject.TryGetProperty("parent", out var parentElement))
            return null;

        if (!parentElement.TryGetProperty("subscription_details", out var subscriptionDetailsElement))
            return null;

        return subscriptionDetailsElement.TryGetProperty("subscription", out var nestedSubscriptionElement)
            ? nestedSubscriptionElement.GetString()
            : null;
    }

    private static string? TryGetInvoicePendingRegistrationPublicId(JsonElement invoiceObject)
    {
        if (!invoiceObject.TryGetProperty("parent", out var parentElement))
            return null;

        if (!parentElement.TryGetProperty("subscription_details", out var subscriptionDetailsElement))
            return null;

        if (!subscriptionDetailsElement.TryGetProperty("metadata", out var metadataElement))
            return null;

        return metadataElement.TryGetProperty("pending_registration_public_id", out var publicIdElement)
            ? publicIdElement.GetString()
            : null;
    }

    private async Task<Result> HandleSubscriptionSyncAsync(string eventId, JsonElement stripeObject,
        CancellationToken cancellationToken)
    {
        var stripeSubscriptionId = stripeObject.TryGetProperty("id", out var idElement)
            ? idElement.GetString()
            : null;
        if (string.IsNullOrWhiteSpace(stripeSubscriptionId))
            return Result.Failure(SubscriptionsError.WebhookPayloadInvalid,
                "Stripe subscription payload does not contain the subscription identifier.");

        var stripePriceId = TryGetNestedString(stripeObject, "items", "data", 0, "price", "id");
        var status = stripeObject.TryGetProperty("status", out var statusElement) ? statusElement.GetString() : null;
        var currentPeriodStart = TryGetUnixDateTimeOffset(stripeObject, "current_period_start");
        var currentPeriodEnd = TryGetUnixDateTimeOffset(stripeObject, "current_period_end");

        if (string.IsNullOrWhiteSpace(stripePriceId))
        {
            var details = await stripeCheckoutService.GetSubscriptionDetailsAsync(stripeSubscriptionId, cancellationToken);
            stripePriceId = details.StripePriceId;
            currentPeriodStart ??= details.CurrentPeriodStart;
            currentPeriodEnd ??= details.CurrentPeriodEnd;
            status ??= details.Status;
        }

        if (string.IsNullOrWhiteSpace(stripePriceId) || string.IsNullOrWhiteSpace(status))
            return Result.Failure(SubscriptionsError.WebhookPayloadInvalid,
                "Stripe subscription payload does not contain enough data to synchronize status.");

        var command = new SyncSubscriptionStatusCommand(
            eventId,
            stripeSubscriptionId,
            stripePriceId,
            status,
            currentPeriodStart,
            currentPeriodEnd);

        return await subscriptionRegistrationCommandService.Handle(command, cancellationToken);
    }

    private bool IsValidSignature(string signatureHeader, string payload)
    {
        if (string.IsNullOrWhiteSpace(_stripeSettings.WebhookSecret) ||
            string.IsNullOrWhiteSpace(signatureHeader) ||
            string.IsNullOrWhiteSpace(payload))
            return false;

        var values = signatureHeader.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var timestampRaw = values.FirstOrDefault(value => value.StartsWith("t=", StringComparison.OrdinalIgnoreCase))
            ?.Split('=')[1];
        var signature = values.FirstOrDefault(value => value.StartsWith("v1=", StringComparison.OrdinalIgnoreCase))
            ?.Split('=')[1];

        if (!long.TryParse(timestampRaw, out var timestamp) || string.IsNullOrWhiteSpace(signature))
            return false;

        var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        if (Math.Abs(now - timestamp) > TimestampToleranceSeconds) return false;

        var signedPayload = $"{timestamp}.{payload}";
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_stripeSettings.WebhookSecret));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(signedPayload));
        var computedSignature = Convert.ToHexStringLower(hash);
        return CryptographicOperations.FixedTimeEquals(
            Encoding.UTF8.GetBytes(computedSignature),
            Encoding.UTF8.GetBytes(signature));
    }

    private static string? TryGetNestedString(JsonElement root, string firstProperty, string secondProperty, int index,
        string thirdProperty, string fourthProperty)
    {
        if (!root.TryGetProperty(firstProperty, out var first)) return null;
        if (!first.TryGetProperty(secondProperty, out var second) || second.ValueKind != JsonValueKind.Array) return null;
        if (second.GetArrayLength() <= index) return null;
        var item = second[index];
        if (!item.TryGetProperty(thirdProperty, out var third)) return null;
        return third.TryGetProperty(fourthProperty, out var fourth) ? fourth.GetString() : null;
    }

    private static DateTimeOffset? TryGetUnixDateTimeOffset(JsonElement root, string propertyName)
    {
        if (!root.TryGetProperty(propertyName, out var property)) return null;
        return property.ValueKind == JsonValueKind.Number && property.TryGetInt64(out var seconds)
            ? DateTimeOffset.FromUnixTimeSeconds(seconds)
            : null;
    }
}
