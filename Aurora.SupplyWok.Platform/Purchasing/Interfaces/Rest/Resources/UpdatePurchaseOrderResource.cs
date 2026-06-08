namespace Aurora.SupplyWok.Platform.Purchasing.Interfaces.Rest.Resources;

public record UpdatePurchaseOrderResource(
    string Code,
    int SupplierId,
    string SupplierName,
    string RestaurantName,
    string OrderDate,
    string? EstimatedDate,
    string Priority,
    string Status,
    IEnumerable<PurchaseOrderItemResource> Items);
