using Aurora.SupplyWok.Platform.Operations.Application.QueryServices;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Operations.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Operations.Application.Internal.QueryServices;

public class DishCategoryQueryService(IDishCategoryRepository dishCategoryRepository) : IDishCategoryQueryService
{
    public async Task<IEnumerable<DishCategory>> Handle(GetAllDishCategoriesQuery query, CancellationToken cancellationToken)
    {
        return await dishCategoryRepository.ListAsync(cancellationToken);
    }
}
