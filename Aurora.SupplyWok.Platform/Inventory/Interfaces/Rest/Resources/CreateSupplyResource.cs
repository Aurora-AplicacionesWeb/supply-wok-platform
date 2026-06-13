namespace Aurora.SupplyWok.Platform.Inventory.Interfaces.Rest.Resources;

public record CreateSupplyResource(
    string Name,
    string UnitOfMeasure,
    int CurrentStock,
    int MinimumStockLevel,
    string Category);
