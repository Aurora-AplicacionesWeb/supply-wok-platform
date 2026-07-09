using Aurora.SupplyWok.Platform.Operations.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.Queries;

namespace Aurora.SupplyWok.Platform.Operations.Application.QueryServices;

public interface IDishQueryService
{
    Task<IEnumerable<Dish>> Handle(GetAllDishesQuery query, CancellationToken cancellationToken);
    Task<Dish?> Handle(GetDishByIdQuery query, CancellationToken cancellationToken);
}
