using Aurora.SupplyWok.Platform.Operations.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Operations.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Operations.Interfaces.Rest.Transform;

public static class DishResourceFromEntityAssembler
{
    public static DishResource ToResourceFromEntity(Dish entity)
    {
        return new DishResource(
            entity.Id,
            entity.Code,
            entity.Name,
            entity.Quantity,
            entity.Description,
            entity.Price,
            entity.Active,
            entity.Outstanding,
            entity.DishCategoryId,
            entity.DishCategory?.Name
        );
    }
}
