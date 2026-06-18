using Aurora.SupplyWok.Platform.Operations.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Operations.Domain.Repositories;

public interface IKitchenOrderRepository : IBaseRepository<KitchenOrder>
{
    Task<KitchenOrder?> FindByIdWithItemsAsync(int id, CancellationToken cancellationToken = default);
}
