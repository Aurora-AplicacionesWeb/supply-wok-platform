using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Profiles.Domain.Model.ValueObjects;
using Aurora.SupplyWok.Platform.Profiles.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Profiles.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to convert an <see cref="UpdateSupplierProfileResource" /> into an <see cref="UpdateSupplierProfileCommand" />.
/// </summary>
public static class UpdateSupplierProfileCommandFromResourceAssembler
{
    public static UpdateSupplierProfileCommand ToCommandFromResource(int supplierProfileId, UpdateSupplierProfileResource resource)
    {
        return new UpdateSupplierProfileCommand(
            supplierProfileId,
            resource.BusinessName,
            new PersonName(resource.FirstName, resource.LastName),
            new StreetAddress(resource.Street, resource.District, resource.City, resource.Country),
            new EmailAddress(resource.ContactEmail),
            resource.Phone,
            resource.Category);
    }
}
