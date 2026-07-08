namespace Aurora.SupplyWok.Platform.Subscriptions.Domain.Model.Commands;

public record ProvisionSubscriptionRegistrationCommand(
    string StripeEventId,
    Guid PendingRegistrationPublicId,
    string StripeCustomerId,
    string StripeSubscriptionId,
    string StripePriceId,
    DateTimeOffset? CurrentPeriodStart,
    DateTimeOffset? CurrentPeriodEnd,
    string StripeSubscriptionStatus);
