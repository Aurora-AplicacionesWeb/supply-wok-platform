using Aurora.SupplyWok.Platform.Operations.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Operations.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Operations.Interfaces.Rest.Transform;

public static class DishCategoryResourceFromEntityAssembler
{
    public static DishCategoryResource ToResourceFromEntity(DishCategory entity)
    {
        return new DishCategoryResource(
            entity.Id,
            entity.Name,
            entity.Order,
            entity.Active
        );
    }
}
