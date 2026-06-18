using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Aggregates;

namespace Aurora.SupplyWok.Platform.Suppliers.Domain.Repositories;

/// <summary>
///     Repository contract for supplier clients.
/// </summary>
public interface IClientRepository : IBaseRepository<Client>
{
    /// <summary>
    ///     Lists all clients linked to the given supplier.
    /// </summary>
    /// <param name="supplierId">The supplier identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The clients linked to the supplier.</returns>
    Task<IEnumerable<Client>> ListBySupplierIdAsync(int supplierId, CancellationToken cancellationToken);
}
