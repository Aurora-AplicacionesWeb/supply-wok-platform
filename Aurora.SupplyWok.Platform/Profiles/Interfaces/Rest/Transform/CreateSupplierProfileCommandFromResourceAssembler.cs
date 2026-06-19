using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Profiles.Domain.Model.ValueObjects;
using Aurora.SupplyWok.Platform.Profiles.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Profiles.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to transform a <see cref="CreateSupplierProfileResource" /> into a
///     <see cref="CreateSupplierProfileCommand" />
/// </summary>
public static class CreateSupplierProfileCommandFromResourceAssembler
{
    /// <summary>
    ///     Build a <see cref="CreateSupplierProfileCommand" /> from a <see cref="CreateSupplierProfileResource" />
    /// </summary>
    /// <param name="resource">
    ///     The resource with the data of the supplier profile to create
    /// </param>
    /// <returns>
    ///     The corresponding <see cref="CreateSupplierProfileCommand" />
    /// </returns>
    public static CreateSupplierProfileCommand ToCommandFromResource(CreateSupplierProfileResource resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        return new CreateSupplierProfileCommand(
            resource.BusinessName,
            new PersonName(resource.FirstName, resource.LastName),
            new StreetAddress(resource.Street, resource.District, resource.City, resource.Country),
            new EmailAddress(resource.ContactEmail),
            resource.UserId);
    }
}
