namespace Aurora.SupplyWok.Platform.Subscriptions.Domain.Model.ValueObjects;

public enum EPendingRegistrationStatus
{
    PendingCheckout,
    CheckoutCompleted,
    Provisioned,
    Expired,
    Failed
}
