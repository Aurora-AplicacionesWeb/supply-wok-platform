namespace Aurora.SupplyWok.Platform.Purchasing.Interfaces.Acl;

public record PurchaseOrderItemAclResource(
    int Id,
    int? InventoryItemId,
    string ProductName,
    decimal Quantity,
    decimal UnitPrice,
    string UnitType);
