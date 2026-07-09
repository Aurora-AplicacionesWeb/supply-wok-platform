using Aurora.SupplyWok.Platform.Operations.Application.QueryServices;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Operations.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Operations.Application.Internal.QueryServices;

public class DishQueryService(IDishRepository dishRepository) : IDishQueryService
{
    public async Task<IEnumerable<Dish>> Handle(GetAllDishesQuery query, CancellationToken cancellationToken)
    {
        return await dishRepository.ListWithCategoryAsync(cancellationToken);
    }

    public async Task<Dish?> Handle(GetDishByIdQuery query, CancellationToken cancellationToken)
    {
        return await dishRepository.FindByIdAsync(query.DishId, cancellationToken);
    }
}
