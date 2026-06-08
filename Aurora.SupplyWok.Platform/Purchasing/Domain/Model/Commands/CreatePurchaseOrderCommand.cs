namespace Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Commands;

public record CreatePurchaseOrderCommand(
    string Code,
    int SupplierId,
    string SupplierName,
    string RestaurantName,
    string OrderDate,
    string? EstimatedDate,
    string Priority,
    string? Status,
    IEnumerable<CreatePurchaseOrderItemCommand> Items);
