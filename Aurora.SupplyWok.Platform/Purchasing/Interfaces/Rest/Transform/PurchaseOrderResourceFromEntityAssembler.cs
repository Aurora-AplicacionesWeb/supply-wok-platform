using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.ValueObjects;
using Aurora.SupplyWok.Platform.Purchasing.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Purchasing.Interfaces.Rest.Transform;

public static class PurchaseOrderResourceFromEntityAssembler
{
    public static PurchaseOrderResource ToResourceFromEntity(PurchaseOrder order)
    {
        ArgumentNullException.ThrowIfNull(order);

        return new PurchaseOrderResource(
            order.Id,
            order.Code,
            order.SupplierId,
            order.SupplierName,
            order.RestaurantName,
            order.OrderDate,
            order.EstimatedDate,
            ToPriorityLabel(order.Priority),
            ToStatusLabel(order.Status),
            order.Items.Select(item => new PurchaseOrderItemResource(
                item.Id,
                item.InventoryItemId,
                item.ProductName,
                item.Quantity,
                item.UnitPrice,
                item.UnitType)));
    }

    private static string ToPriorityLabel(EPurchaseOrderPriority priority)
    {
        return priority.ToString();
    }

    private static string ToStatusLabel(EPurchaseOrderStatus status)
    {
        return status == EPurchaseOrderStatus.InTransit ? "In Transit" : status.ToString();
    }
}
