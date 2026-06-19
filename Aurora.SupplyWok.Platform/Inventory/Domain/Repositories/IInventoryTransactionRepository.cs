using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Inventory.Domain.Repositories;

public interface IInventoryTransactionRepository : IBaseRepository<InventoryTransaction>
{
    Task<IEnumerable<InventoryTransaction>> FindBySupplyIdAsync(int supplyId, CancellationToken cancellationToken);
    Task<InventoryTransaction?> FindWithOperationsByIdAsync(int id, CancellationToken cancellationToken);
}
