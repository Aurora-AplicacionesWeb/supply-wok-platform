using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Aggregate;

/// <summary>
/// Aggregate that calculates supplier SLA based on purchase order delivery outcomes.
/// </summary>
public class PurchaseOrderSla
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PurchaseOrderSla"/> aggregate.
    /// </summary>
    /// <param name="supplierId">The supplier identifier.</param>
    /// <param name="purchaseOrders">The purchase orders used for the SLA calculation.</param>
    public PurchaseOrderSla(int supplierId, IEnumerable<PurchaseOrder> purchaseOrders)
    {
        SupplierId = supplierId;
        var supplierOrders = purchaseOrders.Where(order => order.SupplierId == supplierId).ToList();
        DeliveredOrders = supplierOrders.Count(order => order.Status == EPurchaseOrderStatus.Delivered);
        DelayedOrders = supplierOrders.Count(order => order.Status == EPurchaseOrderStatus.Delayed);
        TotalClosedOrders = DeliveredOrders + DelayedOrders;
        ComplianceRate = TotalClosedOrders == 0 ? 0 : Math.Round((decimal)DeliveredOrders / TotalClosedOrders * 100, 2);
    }

    /// <summary>
    /// Gets the supplier identifier.
    /// </summary>
    public int SupplierId { get; }

    /// <summary>
    /// Gets the count of delivered orders.
    /// </summary>
    public int DeliveredOrders { get; }

    /// <summary>
    /// Gets the count of delayed orders.
    /// </summary>
    public int DelayedOrders { get; }

    /// <summary>
    /// Gets the total count of closed orders considered for SLA.
    /// </summary>
    public int TotalClosedOrders { get; }

    /// <summary>
    /// Gets the SLA compliance rate as a percentage.
    /// </summary>
    public decimal ComplianceRate { get; }
}
