using Aurora.SupplyWok.Platform.Operations.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.Queries;

namespace Aurora.SupplyWok.Platform.Operations.Application.QueryServices;

public interface IKitchenOrderQueryService
{
    Task<IEnumerable<KitchenOrder>> Handle(GetAllKitchenOrdersQuery query, CancellationToken cancellationToken);
    Task<KitchenOrder?> Handle(GetKitchenOrderByIdQuery query, CancellationToken cancellationToken);
}
