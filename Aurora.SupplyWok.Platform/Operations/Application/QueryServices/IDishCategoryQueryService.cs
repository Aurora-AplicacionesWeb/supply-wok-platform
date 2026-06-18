using Aurora.SupplyWok.Platform.Operations.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.Queries;

namespace Aurora.SupplyWok.Platform.Operations.Application.QueryServices;

public interface IDishCategoryQueryService
{
    Task<IEnumerable<DishCategory>> Handle(GetAllDishCategoriesQuery query, CancellationToken cancellationToken);
}
