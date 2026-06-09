using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Commands;

namespace Aurora.SupplyWok.Platform.Purchasing.Interfaces.Acl;

/// <summary>
/// Facade for external access to purchase order capabilities.
/// </summary>
public interface IPurchaseOrderContextFacade
{
    /// <summary>
    /// Creates a purchase order through the Purchasing context.
    /// </summary>
    /// <param name="code">The purchase order code.</param>
    /// <param name="supplierId">The supplier identifier.</param>
    /// <param name="supplierName">The supplier display name.</param>
    /// <param name="restaurantName">The restaurant display name.</param>
    /// <param name="orderDate">The order date in yyyy-MM-dd format.</param>
    /// <param name="estimatedDate">The estimated delivery date in yyyy-MM-dd format.</param>
    /// <param name="priority">The purchase order priority.</param>
    /// <param name="items">The purchase order line items.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created purchase order identifier, or 0 when creation fails.</returns>
    Task<int> CreatePurchaseOrder(
        string code,
        int supplierId,
        string supplierName,
        string restaurantName,
        string orderDate,
        string? estimatedDate,
        string priority,
        IEnumerable<CreatePurchaseOrderItemCommand> items,
        CancellationToken cancellationToken);

    /// <summary>
    /// Updates the status of an existing purchase order.
    /// </summary>
    /// <param name="purchaseOrderId">The purchase order identifier.</param>
    /// <param name="status">The requested status.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True when the status was updated; otherwise false.</returns>
    Task<bool> UpdatePurchaseOrderStatus(int purchaseOrderId, string status, CancellationToken cancellationToken);
}
