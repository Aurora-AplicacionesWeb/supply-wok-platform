namespace Aurora.SupplyWok.Platform.Suppliers.Interfaces.Acl;

public record SupplierOrderAclResource(
    int Id,
    string Code,
    int SupplierId,
    string SupplierName,
    string RestaurantName,
    string OrderDate,
    string EstimatedDate,
    string Priority,
    string Status,
    IEnumerable<SupplierOrderItemAclResource> Items);
