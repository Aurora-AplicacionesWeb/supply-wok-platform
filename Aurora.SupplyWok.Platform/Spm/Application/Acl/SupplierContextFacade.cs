using Aurora.SupplyWok.Platform.Spm.Domain.Repositories;
using Aurora.SupplyWok.Platform.Spm.Interfaces.Acl;

namespace Aurora.SupplyWok.Platform.Spm.Application.Acl;

/// <summary>
/// Application facade for supplier data exposed through the ACL.
/// </summary>
public class SupplierContextFacade(ISupplierRepository supplierRepository) : ISupplierContextFacade
{
    /// <inheritdoc />
    public async Task<SupplierIdentityAclResource?> GetSupplierIdentityById(int supplierId, CancellationToken cancellationToken)
    {
        var supplier = await supplierRepository.FindByIdAsync(supplierId, cancellationToken);
        return supplier is null ? null : new SupplierIdentityAclResource(supplier.Id, supplier.Name);
    }
}
