using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Profiles.Domain.Model.ValueObjects;
using Aurora.SupplyWok.Platform.Profiles.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Profiles.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to transform a <see cref="CreateRestaurantProfileResource" /> into a
///     <see cref="CreateRestaurantProfileCommand" />
/// </summary>
public static class CreateRestaurantProfileCommandFromResourceAssembler
{
    /// <summary>
    ///     Build a <see cref="CreateRestaurantProfileCommand" /> from a <see cref="CreateRestaurantProfileResource" />
    /// </summary>
    /// <param name="resource">
    ///     The resource with the data of the restaurant profile to create
    /// </param>
    /// <returns>
    ///     The corresponding <see cref="CreateRestaurantProfileCommand" />
    /// </returns>
    public static CreateRestaurantProfileCommand ToCommandFromResource(CreateRestaurantProfileResource resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        return new CreateRestaurantProfileCommand(
            resource.BusinessName,
            new PersonName(resource.FirstName, resource.LastName),
            new StreetAddress(resource.Street, resource.District, resource.City, resource.Country),
            new EmailAddress(resource.ContactEmail),
            resource.UserId);
    }
}
