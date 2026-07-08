using Aurora.SupplyWok.Platform.Subscriptions.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Subscriptions.Domain.Model.ValueObjects;
using Aurora.SupplyWok.Platform.Subscriptions.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Subscriptions.Interfaces.Rest.Transform;

public static class SubscriptionRegistrationResourceFromEntityAssembler
{
    public static SubscriptionRegistrationResource ToResourceFromEntity(PendingRegistration pendingRegistration,
        string checkoutUrl)
    {
        return new SubscriptionRegistrationResource(
            pendingRegistration.PublicId,
            pendingRegistration.Status.ToString(),
            checkoutUrl);
    }

    public static SubscriptionRegistrationStatusResource ToStatusResourceFromEntity(PendingRegistration pendingRegistration)
    {
        return new SubscriptionRegistrationStatusResource(
            pendingRegistration.PublicId,
            pendingRegistration.Status.ToString(),
            pendingRegistration.Email,
            pendingRegistration.Role,
            pendingRegistration.PlanCode.ToString(),
            pendingRegistration.Status is EPendingRegistrationStatus.PendingCheckout or EPendingRegistrationStatus.Failed
                or EPendingRegistrationStatus.Expired,
            pendingRegistration.Status == EPendingRegistrationStatus.Provisioned);
    }
}
