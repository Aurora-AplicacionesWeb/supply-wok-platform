using Aurora.SupplyWok.Platform.Operations.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Operations.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Operations.Interfaces.Rest.Transform;

public static class KitchenOrderResourceFromEntityAssembler
{
    public static KitchenOrderResource ToResourceFromEntity(KitchenOrder entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        return new KitchenOrderResource(
            entity.Id,
            entity.Number,
            entity.TableId,
            entity.TypeService,
            entity.Status,
            entity.Observations,
            entity.DateCreated,
            entity.HourReady,
            entity.HourDelivered,
            entity.PreparationTime,
            entity.TotalPrice,
            entity.Items.Select(item => new KitchenOrderItemResource(
                item.Id,
                item.DishId,
                item.DishName,
                item.Quantity,
                item.UnitPrice,
                item.SubTotal)).ToList());
    }
}
