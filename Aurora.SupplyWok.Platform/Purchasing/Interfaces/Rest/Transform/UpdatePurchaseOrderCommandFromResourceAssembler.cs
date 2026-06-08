using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Purchasing.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Purchasing.Interfaces.Rest.Transform;

public static class UpdatePurchaseOrderCommandFromResourceAssembler
{
    public static UpdatePurchaseOrderCommand ToCommandFromResource(int id, UpdatePurchaseOrderResource resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        return new UpdatePurchaseOrderCommand(
            id,
            resource.Code,
            resource.SupplierId,
            resource.SupplierName,
            resource.RestaurantName,
            resource.OrderDate,
            resource.EstimatedDate,
            resource.Priority,
            resource.Status,
            (resource.Items ?? Enumerable.Empty<PurchaseOrderItemResource>()).Select(ToItemCommand));
    }

    private static CreatePurchaseOrderItemCommand ToItemCommand(PurchaseOrderItemResource resource)
    {
        return new CreatePurchaseOrderItemCommand(resource.Id, resource.InventoryItemId, resource.ProductName,
            resource.Quantity, resource.UnitPrice, resource.UnitType);
    }
}
