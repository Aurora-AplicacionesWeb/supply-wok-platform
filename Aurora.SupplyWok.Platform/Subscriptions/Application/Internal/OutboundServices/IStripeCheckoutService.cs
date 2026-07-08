namespace Aurora.SupplyWok.Platform.Subscriptions.Application.Internal.OutboundServices;

public interface IStripeCheckoutService
{
    Task<(bool IsSuccess, string? SessionId, string? CheckoutUrl, string? ErrorMessage)> CreateSubscriptionCheckoutSessionAsync(
        Guid pendingRegistrationPublicId,
        string email,
        string role,
        string planCode,
        CancellationToken cancellationToken);

    Task<(bool IsSuccess, string? PendingRegistrationPublicId, string? StripePriceId, DateTimeOffset? CurrentPeriodStart, DateTimeOffset? CurrentPeriodEnd, string? Status)> GetSubscriptionDetailsAsync(
        string stripeSubscriptionId,
        CancellationToken cancellationToken);
}
