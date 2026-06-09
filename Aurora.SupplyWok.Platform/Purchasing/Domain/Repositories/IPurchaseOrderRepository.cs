using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Purchasing.Domain.Repositories;

/// <summary>
/// Repository contract for purchase order persistence operations.
/// </summary>
public interface IPurchaseOrderRepository : IBaseRepository<PurchaseOrder>
{
    /// <summary>
    /// Lists all purchase orders including their line items.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The collection of purchase orders.</returns>
    Task<IEnumerable<PurchaseOrder>> ListPurchaseOrdersAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Gets a purchase order by its unique identifier including its line items.
    /// </summary>
    /// <param name="id">The purchase order identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The purchase order if found; otherwise null.</returns>
    Task<PurchaseOrder?> GetPurchaseOrderByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Determines whether a purchase order code is already used.
    /// </summary>
    /// <param name="code">The purchase order code to check.</param>
    /// <param name="excludedId">The purchase order identifier to exclude when validating updates.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True when another purchase order uses the code; otherwise false.</returns>
    Task<bool> ExistsByCodeAsync(string code, int? excludedId, CancellationToken cancellationToken);
}
