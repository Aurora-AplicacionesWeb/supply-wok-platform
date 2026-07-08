using Aurora.SupplyWok.Platform.Subscriptions.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Subscriptions.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Subscriptions.Interfaces.Rest.Transform;

public static class StartSubscriptionRegistrationCommandFromResourceAssembler
{
    public static StartSubscriptionRegistrationCommand ToCommandFromResource(
        StartSubscriptionRegistrationResource resource)
    {
        return new StartSubscriptionRegistrationCommand(
            resource.Email,
            resource.Password,
            resource.Role,
            resource.PlanCode,
            resource.BusinessName,
            resource.FirstName,
            resource.LastName,
            resource.Street,
            resource.District,
            resource.City,
            string.IsNullOrWhiteSpace(resource.Country) ? "Peru" : resource.Country,
            resource.ContactEmail,
            resource.Phone,
            resource.Category);
    }
}
