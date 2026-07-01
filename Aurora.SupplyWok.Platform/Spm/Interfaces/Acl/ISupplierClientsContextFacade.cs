namespace Aurora.SupplyWok.Platform.Spm.Interfaces.Acl;

/// <summary>
/// Facade for supplier-centered client read operations.
/// </summary>
public interface ISupplierClientsContextFacade
{
    /// <summary>
    /// Gets every client linked to a supplier.
    /// </summary>
    /// <param name="supplierId">The supplier identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The clients associated with the supplier.</returns>
    Task<IEnumerable<ClientAclResource>> GetClientsBySupplierId(int supplierId, CancellationToken cancellationToken);
}
