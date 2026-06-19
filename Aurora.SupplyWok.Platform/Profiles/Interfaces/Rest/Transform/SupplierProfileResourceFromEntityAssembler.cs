using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Profiles.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Profiles.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to transform a <see cref="SupplierProfile" /> into a <see cref="SupplierProfileResource" />
/// </summary>
public static class SupplierProfileResourceFromEntityAssembler
{
    /// <summary>
    ///     Build a <see cref="SupplierProfileResource" /> from a <see cref="SupplierProfile" />
    /// </summary>
    /// <param name="supplierProfile">
    ///     The supplier profile entity to transform
    /// </param>
    /// <returns>
    ///     The corresponding <see cref="SupplierProfileResource" />
    /// </returns>
    public static SupplierProfileResource ToResourceFromEntity(SupplierProfile supplierProfile)
    {
        ArgumentNullException.ThrowIfNull(supplierProfile);

        return new SupplierProfileResource(
            supplierProfile.Id,
            supplierProfile.BusinessName,
            supplierProfile.ContactName.FirstName,
            supplierProfile.ContactName.LastName,
            supplierProfile.Address.Street,
            supplierProfile.Address.District,
            supplierProfile.Address.City,
            supplierProfile.Address.Country,
            supplierProfile.ContactEmail.Address,
            supplierProfile.Status,
            supplierProfile.UserId);
    }
}
