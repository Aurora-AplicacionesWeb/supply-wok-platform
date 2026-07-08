namespace Aurora.SupplyWok.Platform.Subscriptions.Domain.Model.ValueObjects;

public enum ESubscriptionStatus
{
    Active,
    PastDue,
    Unpaid,
    Canceled,
    Incomplete
}
