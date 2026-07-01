using Aurora.SupplyWok.Platform.Purchasing.Interfaces.Acl;
using Aurora.SupplyWok.Platform.Spm.Interfaces.Acl;

namespace Aurora.SupplyWok.Platform.Spm.Application.Acl;

/// <summary>
/// Supplier-facing facade that retrieves purchase order data through the Purchasing ACL.
/// </summary>
public class SupplierOrdersContextFacade(IPurchaseOrderContextFacade purchaseOrderContextFacade) : ISupplierOrdersContextFacade
{
    /// <inheritdoc />
    public async Task<IEnumerable<SupplierOrderAclResource>> GetOrdersBySupplierId(int supplierId, CancellationToken cancellationToken)
    {
        var orders = await purchaseOrderContextFacade.GetPurchaseOrdersBySupplierId(supplierId, cancellationToken);

        return orders.Select(order => new SupplierOrderAclResource(
            order.Id,
            order.Code,
            order.SupplierId,
            order.SupplierName,
            order.RestaurantName,
            order.OrderDate,
            order.EstimatedDate,
            order.Priority,
            order.Status,
            order.Items.Select(item => new SupplierOrderItemAclResource(
                item.Id,
                item.InventoryItemId,
                item.ProductName,
                item.Quantity,
                item.UnitPrice,
                item.UnitType))));
    }
}
