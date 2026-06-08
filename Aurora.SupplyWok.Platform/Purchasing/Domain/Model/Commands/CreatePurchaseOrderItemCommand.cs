namespace Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Commands;

public record CreatePurchaseOrderItemCommand(
    long? Id,
    int? InventoryItemId,
    string ProductName,
    decimal Quantity,
    decimal UnitPrice,
    string UnitType);
