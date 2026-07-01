namespace Aurora.SupplyWok.Platform.Spm.Interfaces.Acl;

public record SupplierOrderItemAclResource(
    int Id,
    int? InventoryItemId,
    string ProductName,
    decimal Quantity,
    decimal UnitPrice,
    string UnitType);
