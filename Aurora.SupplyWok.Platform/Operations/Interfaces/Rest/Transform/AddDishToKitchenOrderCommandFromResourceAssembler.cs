using Aurora.SupplyWok.Platform.Operations.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Operations.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Operations.Interfaces.Rest.Transform;

public static class AddDishToKitchenOrderCommandFromResourceAssembler
{
    public static AddDishToKitchenOrderCommand ToCommandFromResource(AddDishToKitchenOrderResource resource, int kitchenOrderId)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource));
        return new AddDishToKitchenOrderCommand(kitchenOrderId, resource.DishId, resource.Quantity);
    }
}
