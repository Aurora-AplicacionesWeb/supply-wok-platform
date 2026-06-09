using Aurora.SupplyWok.Platform.Purchasing.Application.CommandServices;
using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Purchasing.Interfaces.Acl;

namespace Aurora.SupplyWok.Platform.Purchasing.Application.Ad;

/// <summary>
/// Application facade for purchase order operations exposed through the ACL.
/// </summary>
public class PurchaseOrderContextFacade(IPurchaseOrderCommandService purchaseOrderCommandService) : IPurchaseOrderContextFacade
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
    public async Task<bool> UpdatePurchaseOrderStatus(int purchaseOrderId, string status, CancellationToken cancellationToken)
    {
        var command = new UpdatePurchaseOrderStatusCommand(purchaseOrderId, status);
        var result = await purchaseOrderCommandService.Handle(command, cancellationToken);
        return result.IsSuccess;
    }
}
