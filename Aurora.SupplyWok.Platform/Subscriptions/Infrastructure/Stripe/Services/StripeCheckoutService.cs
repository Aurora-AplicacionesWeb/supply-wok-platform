using System.Net.Http.Headers;
using System.Text.Json;
using Aurora.SupplyWok.Platform.Subscriptions.Application.Internal.OutboundServices;
using Aurora.SupplyWok.Platform.Subscriptions.Infrastructure.Stripe.Configuration;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace Aurora.SupplyWok.Platform.Subscriptions.Infrastructure.Stripe.Services;

public class StripeCheckoutService(
    HttpClient httpClient,
    IOptions<StripeSettings> stripeOptions,
    IOptions<FrontendUrlsSettings> frontendOptions) : IStripeCheckoutService
{
    private readonly StripeSettings _stripeSettings = stripeOptions.Value;
    private readonly FrontendUrlsSettings _frontendSettings = frontendOptions.Value;

    public async Task<(bool IsSuccess, string? SessionId, string? CheckoutUrl, string? ErrorMessage)>
        CreateSubscriptionCheckoutSessionAsync(
            Guid pendingRegistrationPublicId,
            string email,
            string role,
            string planCode,
            CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_stripeSettings.SecretKey))
            return (false, null, null, "Stripe secret key is not configured.");

        var priceId = ResolvePriceId(planCode);
        if (string.IsNullOrWhiteSpace(priceId))
            return (false, null, null, "Stripe price id is not configured for the selected plan.");

        var successUrl = BuildSuccessUrl(pendingRegistrationPublicId);
        var cancelUrl = BuildCancelUrl();
        var body = new Dictionary<string, string>
        {
            ["mode"] = "subscription",
            ["success_url"] = successUrl,
            ["cancel_url"] = cancelUrl,
            ["client_reference_id"] = pendingRegistrationPublicId.ToString(),
            ["customer_email"] = email,
            ["line_items[0][price]"] = priceId,
            ["line_items[0][quantity]"] = "1",
            ["metadata[pending_registration_public_id]"] = pendingRegistrationPublicId.ToString(),
            ["metadata[role]"] = role,
            ["metadata[plan_code]"] = planCode,
            ["subscription_data[metadata][pending_registration_public_id]"] = pendingRegistrationPublicId.ToString(),
            ["subscription_data[metadata][role]"] = role,
            ["subscription_data[metadata][plan_code]"] = planCode
        };

        using var request = new HttpRequestMessage(HttpMethod.Post, "https://api.stripe.com/v1/checkout/sessions");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _stripeSettings.SecretKey);
        request.Content = new FormUrlEncodedContent(body);

        using var response = await httpClient.SendAsync(request, cancellationToken);
        var payload = await response.Content.ReadAsStringAsync(cancellationToken);
        if (!response.IsSuccessStatusCode)
            return (false, null, null, payload);

        using var document = JsonDocument.Parse(payload);
        var sessionId = document.RootElement.TryGetProperty("id", out var idElement) ? idElement.GetString() : null;
        var checkoutUrl = document.RootElement.TryGetProperty("url", out var urlElement) ? urlElement.GetString() : null;
        return string.IsNullOrWhiteSpace(sessionId) || string.IsNullOrWhiteSpace(checkoutUrl)
            ? (false, null, null, "Stripe checkout session response was incomplete.")
            : (true, sessionId, checkoutUrl, null);
    }

    public async Task<(bool IsSuccess, string? PendingRegistrationPublicId, string? StripePriceId,
        DateTimeOffset? CurrentPeriodStart, DateTimeOffset? CurrentPeriodEnd, string? Status)> GetSubscriptionDetailsAsync(
        string stripeSubscriptionId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_stripeSettings.SecretKey))
            return (false, null, null, null, null, null);

        using var request = new HttpRequestMessage(HttpMethod.Get,
            $"https://api.stripe.com/v1/subscriptions/{stripeSubscriptionId}");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _stripeSettings.SecretKey);

        using var response = await httpClient.SendAsync(request, cancellationToken);
        var payload = await response.Content.ReadAsStringAsync(cancellationToken);
        if (!response.IsSuccessStatusCode) return (false, null, null, null, null, null);

        using var document = JsonDocument.Parse(payload);
        var root = document.RootElement;
        var metadata = root.TryGetProperty("metadata", out var metadataElement) ? metadataElement : default;
        var pendingRegistrationPublicId = metadata.ValueKind != JsonValueKind.Undefined &&
                                          metadata.TryGetProperty("pending_registration_public_id",
                                              out var publicIdElement)
            ? publicIdElement.GetString()
            : null;
        var stripePriceId = TryGetNestedString(root, "items", "data", 0, "price", "id");
        var status = root.TryGetProperty("status", out var statusElement) ? statusElement.GetString() : null;
        var currentPeriodStart = TryGetUnixDateTimeOffset(root, "current_period_start")
                                 ?? TryGetNestedUnixDateTimeOffset(root, "items", "data", 0, "current_period_start");
        var currentPeriodEnd = TryGetUnixDateTimeOffset(root, "current_period_end")
                               ?? TryGetNestedUnixDateTimeOffset(root, "items", "data", 0, "current_period_end");

        return (true, pendingRegistrationPublicId, stripePriceId, currentPeriodStart, currentPeriodEnd, status);
    }

    private string ResolvePriceId(string planCode)
    {
        return planCode.Trim().ToLowerInvariant() switch
        {
            "premium" => _stripeSettings.Prices.PremiumMonthly,
            "enterprise" => _stripeSettings.Prices.EnterpriseMonthly,
            _ => string.Empty
        };
    }

    private string BuildSuccessUrl(Guid pendingRegistrationPublicId)
    {
        var path = _frontendSettings.RegisterCompletePath.Trim();
        var baseUrl = _frontendSettings.BaseUrl.TrimEnd('/');
        return QueryHelpers.AddQueryString($"{baseUrl}{path}", "registration", pendingRegistrationPublicId.ToString());
    }

    private string BuildCancelUrl()
    {
        return $"{_frontendSettings.BaseUrl.TrimEnd('/')}{_frontendSettings.RegisterPath.Trim()}";
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

    private static DateTimeOffset? TryGetNestedUnixDateTimeOffset(JsonElement root, string firstProperty,
        string secondProperty, int index, string thirdProperty)
    {
        if (!root.TryGetProperty(firstProperty, out var first)) return null;
        if (!first.TryGetProperty(secondProperty, out var second) || second.ValueKind != JsonValueKind.Array) return null;
        if (second.GetArrayLength() <= index) return null;

        var item = second[index];
        if (!item.TryGetProperty(thirdProperty, out var property)) return null;
        return property.ValueKind == JsonValueKind.Number && property.TryGetInt64(out var seconds)
            ? DateTimeOffset.FromUnixTimeSeconds(seconds)
            : null;
    }
}
