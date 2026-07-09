using System.Net.Http.Headers;
using System.Text.Json;
using Aurora.SupplyWok.Platform.Subscriptions.Application.Internal.OutboundServices;
using Aurora.SupplyWok.Platform.Subscriptions.Infrastructure.Stripe.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace Aurora.SupplyWok.Platform.Subscriptions.Infrastructure.Stripe.Services;

public class StripeCheckoutService(
    HttpClient httpClient,
    IOptions<StripeSettings> stripeOptions,
    IOptions<FrontendUrlsSettings> frontendOptions,
    IHttpContextAccessor httpContextAccessor) : IStripeCheckoutService
{
    private readonly StripeSettings _stripeSettings = stripeOptions.Value;
    private readonly FrontendUrlsSettings _frontendSettings = frontendOptions.Value;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

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
        var path = NormalizePath(_frontendSettings.RegisterCompletePath, "/register/complete");
        var baseUrl = ResolveFrontendBaseUrl();
        return QueryHelpers.AddQueryString($"{baseUrl}{path}", "registration", pendingRegistrationPublicId.ToString());
    }

    private string BuildCancelUrl()
    {
        return $"{ResolveFrontendBaseUrl()}{NormalizePath(_frontendSettings.RegisterPath, "/register")}";
    }

    private string ResolveFrontendBaseUrl()
    {
        var configuredBaseUrl = _frontendSettings.BaseUrl?.Trim();
        if (IsAbsoluteHttpUrl(configuredBaseUrl) && !IsLocalhostUrl(configuredBaseUrl!))
            return configuredBaseUrl!.TrimEnd('/');

        var originHeader = _httpContextAccessor.HttpContext?.Request.Headers.Origin.FirstOrDefault();
        if (IsAbsoluteHttpUrl(originHeader)) return originHeader!.TrimEnd('/');

        var refererHeader = _httpContextAccessor.HttpContext?.Request.Headers.Referer.FirstOrDefault();
        if (Uri.TryCreate(refererHeader, UriKind.Absolute, out var refererUri) &&
            (refererUri.Scheme == Uri.UriSchemeHttp || refererUri.Scheme == Uri.UriSchemeHttps))
            return $"{refererUri.Scheme}://{refererUri.Authority}";

        if (IsAbsoluteHttpUrl(configuredBaseUrl))
            return configuredBaseUrl!.TrimEnd('/');

        throw new InvalidOperationException(
            "Frontend base URL is not configured with a valid absolute URL and could not be inferred from the request.");
    }

    private static string NormalizePath(string? path, string fallback)
    {
        var normalized = string.IsNullOrWhiteSpace(path) ? fallback : path.Trim();
        return normalized.StartsWith('/') ? normalized : $"/{normalized}";
    }

    private static bool IsAbsoluteHttpUrl(string? value)
    {
        return Uri.TryCreate(value, UriKind.Absolute, out var uri) &&
               (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
    }

    private static bool IsLocalhostUrl(string value)
    {
        return Uri.TryCreate(value, UriKind.Absolute, out var uri) &&
               (string.Equals(uri.Host, "localhost", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(uri.Host, "127.0.0.1", StringComparison.OrdinalIgnoreCase));
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
