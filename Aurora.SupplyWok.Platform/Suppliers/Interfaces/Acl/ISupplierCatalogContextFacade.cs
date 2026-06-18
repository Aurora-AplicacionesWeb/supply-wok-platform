namespace Aurora.SupplyWok.Platform.Suppliers.Interfaces.Acl;

/// <summary>
/// Facade for supplier-centered catalog read operations.
/// </summary>
public interface ISupplierCatalogContextFacade
{
    /// <summary>
    /// Gets every catalog item owned by a supplier.
    /// </summary>
    /// <param name="supplierId">The supplier identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The catalog items associated with the supplier.</returns>
    Task<IEnumerable<CatalogItemAclResource>> GetCatalogItemsBySupplierId(int supplierId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets a single catalog item owned by a supplier.
    /// </summary>
    /// <param name="supplierId">The supplier identifier.</param>
    /// <param name="catalogItemId">The catalog item identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The catalog item when found; otherwise null.</returns>
    Task<CatalogItemAclResource?> GetCatalogItemById(int supplierId, int catalogItemId, CancellationToken cancellationToken);
}
