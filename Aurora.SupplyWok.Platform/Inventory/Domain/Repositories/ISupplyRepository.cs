using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;
namespace Aurora.SupplyWok.Platform.Inventory.Domain.Repositories;

public interface ISupplyRepository : IBaseRepository<Supply>
{
    Task<Supply?> GetSupplyByIdAsync(int id, CancellationToken cancellationToken);
    Task<int> GetTotalCurrentStockAsync(CancellationToken cancellationToken);
    Task AddStockMovementAsync(StockMovement movement, CancellationToken cancellationToken);
    Task<IEnumerable<StockMovement>> ListStockMovementsAsync(CancellationToken cancellationToken);
    Task<StockMovement?> GetStockMovementByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<StockMovement>> ListStockMovementsBySupplyIdAsync(int supplyId, CancellationToken cancellationToken);
}
