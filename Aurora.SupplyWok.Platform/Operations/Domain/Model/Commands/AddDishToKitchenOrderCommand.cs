namespace Aurora.SupplyWok.Platform.Operations.Domain.Model.Commands;

public record AddDishToKitchenOrderCommand(int KitchenOrderId, int DishId, int Quantity);
