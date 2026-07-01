namespace Aurora.SupplyWok.Platform.Spm.Interfaces.Acl;

/// <summary>
/// Facade for supplier-centered order read operations.
/// </summary>
public interface ISupplierOrdersContextFacade
{
    /// <summary>
    /// Gets every purchase order associated with a supplier.
    /// </summary>
    /// <param name="supplierId">The supplier identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The orders associated with the supplier.</returns>
    Task<IEnumerable<SupplierOrderAclResource>> GetOrdersBySupplierId(int supplierId, CancellationToken cancellationToken);
}
