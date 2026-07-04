using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Profiles.Domain.Model.ValueObjects;
using Aurora.SupplyWok.Platform.Profiles.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Profiles.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to convert an <see cref="UpdateRestaurantProfileResource" /> into an <see cref="UpdateRestaurantProfileCommand" />.
/// </summary>
public static class UpdateRestaurantProfileCommandFromResourceAssembler
{
    public static UpdateRestaurantProfileCommand ToCommandFromResource(int restaurantProfileId, UpdateRestaurantProfileResource resource)
    {
        return new UpdateRestaurantProfileCommand(
            restaurantProfileId,
            resource.BusinessName,
            new PersonName(resource.FirstName, resource.LastName),
            new StreetAddress(resource.Street, resource.District, resource.City, resource.Country),
            new EmailAddress(resource.ContactEmail));
    }
}
