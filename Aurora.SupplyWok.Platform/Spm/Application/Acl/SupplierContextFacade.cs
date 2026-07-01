using Aurora.SupplyWok.Platform.Spm.Domain.Repositories;
using Aurora.SupplyWok.Platform.Spm.Interfaces.Acl;

namespace Aurora.SupplyWok.Platform.Spm.Application.Acl;

/// <summary>
/// Application facade for supplier data exposed through the ACL.
/// </summary>
/// <remarks>
/// TODO: This facade currently reads from the local SupplierReference repository.
/// In a future iteration it should delegate to the Profiles bounded context
/// (via IProfilesContextFacade) to retrieve supplier identity data,
/// since the full supplier profile is owned by Profiles.
/// </remarks>
public class SupplierContextFacade(ISupplierRepository supplierRepository) : ISupplierContextFacade
{
    /// <inheritdoc />
    public async Task<SupplierIdentityAclResource?> GetSupplierIdentityById(int supplierId, CancellationToken cancellationToken)
    {
        var supplier = await supplierRepository.FindByIdAsync(supplierId, cancellationToken);
        return supplier is null ? null : new SupplierIdentityAclResource(supplier.Id, supplier.Name);
    }
}
