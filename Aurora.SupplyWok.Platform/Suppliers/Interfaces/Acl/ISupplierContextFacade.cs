namespace Aurora.SupplyWok.Platform.Suppliers.Interfaces.Acl;

/// <summary>
/// Facade for external access to supplier identity data.
/// </summary>
public interface ISupplierContextFacade
{
    /// <summary>
    /// Gets the supplier identity required by other bounded contexts.
    /// </summary>
    /// <param name="supplierId">The supplier identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The supplier identity if found; otherwise null.</returns>
    Task<SupplierIdentityAclResource?> GetSupplierIdentityById(int supplierId, CancellationToken cancellationToken);
}
