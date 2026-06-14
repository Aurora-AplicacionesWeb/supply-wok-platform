namespace Aurora.SupplyWok.Platform.Inventory.Interfaces.Rest.Resources;

public record UpdateSupplyResource(
    string Name,
    string UnitOfMeasure,
    int MinimumStockLevel,
    string Category);
