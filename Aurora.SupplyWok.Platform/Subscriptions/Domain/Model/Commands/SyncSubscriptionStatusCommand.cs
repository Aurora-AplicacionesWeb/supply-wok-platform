namespace Aurora.SupplyWok.Platform.Subscriptions.Domain.Model.Commands;

public record SyncSubscriptionStatusCommand(
    string StripeEventId,
    string StripeSubscriptionId,
    string StripePriceId,
    string StripeSubscriptionStatus,
    DateTimeOffset? CurrentPeriodStart,
    DateTimeOffset? CurrentPeriodEnd);
