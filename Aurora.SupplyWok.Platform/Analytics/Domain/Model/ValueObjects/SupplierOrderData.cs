namespace Aurora.SupplyWok.Platform.Analytics.Domain.Model.ValueObjects;

public record SupplierOrderItemData(int? InventoryItemId, decimal Quantity);

public record SupplierOrderData(
    string OrderDate,
    string RestaurantName,
    string Status,
    IEnumerable<SupplierOrderItemData> Items);
