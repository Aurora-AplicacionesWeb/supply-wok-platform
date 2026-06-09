namespace Aurora.SupplyWok.Platform.Purchasing.Interfaces.Acl;

/// <summary>
/// Facade for external access to supplier capabilities in the Purchasing context.
/// </summary>
public interface ISupplierContextFacade
{
    /// <summary>
    /// Calculates the supplier SLA percentage from purchase order outcomes.
    /// </summary>
    /// <param name="supplierId">The supplier identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The SLA compliance rate as a percentage.</returns>
    Task<decimal> CalculateSupplierSla(int supplierId, CancellationToken cancellationToken);
}
