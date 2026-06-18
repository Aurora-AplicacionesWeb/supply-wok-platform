using Aurora.SupplyWok.Platform.Purchasing.Application.CommandServices;
using Aurora.SupplyWok.Platform.Purchasing.Application.QueryServices;
using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Purchasing.Interfaces.Acl;

namespace Aurora.SupplyWok.Platform.Purchasing.Application.Acl;

/// <summary>
/// Application facade for purchase order operations exposed through the ACL.
/// </summary>
public class PurchaseOrderContextFacade(
    IPurchaseOrderCommandService purchaseOrderCommandService,
    IPurchaseOrderQueryService purchaseOrderQueryService) : IPurchaseOrderContextFacade
{
    /// <inheritdoc />
    public async Task<int> CreatePurchaseOrder(
        string code,
        int supplierId,
        string supplierName,
        string restaurantName,
        string orderDate,
        string? estimatedDate,
        string priority,
        IEnumerable<CreatePurchaseOrderItemCommand> items,
        CancellationToken cancellationToken)
    {
        var command = new CreatePurchaseOrderCommand(code, supplierId, supplierName, restaurantName, orderDate,
            estimatedDate, priority, "Pending", items);
        var result = await purchaseOrderCommandService.Handle(command, cancellationToken);
        return result.Value?.Id ?? 0;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<PurchaseOrderAclResource>> GetPurchaseOrdersBySupplierId(int supplierId, CancellationToken cancellationToken)
    {
        var orders = await purchaseOrderQueryService.Handle(new GetPurchaseOrdersBySupplierIdQuery(supplierId), cancellationToken);
        return orders.Select(ToAclResource);
    }

    /// <inheritdoc />
    public async Task<bool> UpdatePurchaseOrderStatus(int purchaseOrderId, string status, CancellationToken cancellationToken)
    {
        var command = new UpdatePurchaseOrderStatusCommand(purchaseOrderId, status);
        var result = await purchaseOrderCommandService.Handle(command, cancellationToken);
        return result.IsSuccess;
    }

    private static PurchaseOrderAclResource ToAclResource(PurchaseOrder order)
    {
        return new PurchaseOrderAclResource(
            order.Id,
            order.Code,
            order.SupplierId,
            order.SupplierName,
            order.RestaurantName,
            order.OrderDate,
            order.EstimatedDate,
            order.Priority.ToString(),
            order.Status.ToString(),
            order.Items.Select(item => new PurchaseOrderItemAclResource(
                item.Id,
                item.InventoryItemId,
                item.ProductName,
                item.Quantity,
                item.UnitPrice,
                item.UnitType)));
    }
}
