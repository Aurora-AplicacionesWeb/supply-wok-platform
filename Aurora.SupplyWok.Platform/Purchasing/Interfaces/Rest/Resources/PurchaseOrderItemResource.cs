namespace Aurora.SupplyWok.Platform.Purchasing.Interfaces.Rest.Resources;

public record PurchaseOrderItemResource(
    long? Id,
    int? InventoryItemId,
    string ProductName,
    decimal Quantity,
    decimal UnitPrice,
    string UnitType);
