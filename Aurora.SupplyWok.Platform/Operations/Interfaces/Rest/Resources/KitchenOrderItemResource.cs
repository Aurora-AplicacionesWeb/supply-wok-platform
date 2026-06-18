namespace Aurora.SupplyWok.Platform.Operations.Interfaces.Rest.Resources;

public record KitchenOrderItemResource(
    int Id,
    int DishId,
    string DishName,
    int Quantity,
    double UnitPrice,
    double SubTotal
);
