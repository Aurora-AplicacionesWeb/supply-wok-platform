using Aurora.SupplyWok.Platform.Operations.Application.QueryServices;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Operations.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Operations.Application.Internal.QueryServices;

public class KitchenOrderQueryService(IKitchenOrderRepository kitchenOrderRepository) : IKitchenOrderQueryService
{
    public async Task<IEnumerable<KitchenOrder>> Handle(GetAllKitchenOrdersQuery query, CancellationToken cancellationToken)
    {
        return await kitchenOrderRepository.ListAsync(cancellationToken);
    }

    public async Task<KitchenOrder?> Handle(GetKitchenOrderByIdQuery query, CancellationToken cancellationToken)
    {
        return await kitchenOrderRepository.FindByIdWithItemsAsync(query.Id, cancellationToken);
    }
}
