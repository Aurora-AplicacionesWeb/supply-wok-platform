namespace Aurora.SupplyWok.Platform.Purchasing.Interfaces.Acl;

public record PurchaseOrderAclResource(
    int Id,
    string Code,
    int SupplierId,
    string SupplierName,
    string RestaurantName,
    string OrderDate,
    string EstimatedDate,
    string Priority,
    string Status,
    IEnumerable<PurchaseOrderItemAclResource> Items);
