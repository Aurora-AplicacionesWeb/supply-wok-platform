using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Inventory.Domain.Repositories;

public interface ISupplyRepository : IBaseRepository<Supply>
{
    Task<Supply?> GetSupplyByIdAsync(int id, CancellationToken cancellationToken);
    Task<int> GetTotalCurrentStockAsync(CancellationToken cancellationToken);
}
