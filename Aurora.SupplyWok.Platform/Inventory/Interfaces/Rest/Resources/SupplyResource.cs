namespace Aurora.SupplyWok.Platform.Inventory.Interfaces.Rest.Resources;

public record SupplyResource(
    int Id,
    string Name,
    string UnitOfMeasure,
    int CurrentStock,
    int MinimumStockLevel,
    string Category);
