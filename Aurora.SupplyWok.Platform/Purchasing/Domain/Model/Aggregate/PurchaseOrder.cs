using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.ValueObjects;
using Aurora.SupplyWok.Platform.Shared.Domain.Model.Entities;

namespace Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Aggregate;

/// <summary>
/// Represents a purchase order in the Supply Wok purchasing flow.
/// </summary>
public class PurchaseOrder : IAuditableEntity
{
    public PurchaseOrder()
    {
        Code = string.Empty;
        SupplierName = string.Empty;
        RestaurantName = string.Empty;
        OrderDate = string.Empty;
        EstimatedDate = string.Empty;
        Priority = EPurchaseOrderPriority.Medium;
        Status = EPurchaseOrderStatus.Pending;
        Items = new List<PurchaseOrderItem>();
    }

    public PurchaseOrder(
        string code,
        int supplierId,
        string supplierName,
        string restaurantName,
        string orderDate,
        string? estimatedDate,
        EPurchaseOrderPriority priority,
        EPurchaseOrderStatus status,
        IEnumerable<PurchaseOrderItem> items) : this()
    {
        Code = code;
        SupplierId = supplierId;
        SupplierName = supplierName;
        RestaurantName = restaurantName;
        OrderDate = orderDate;
        EstimatedDate = estimatedDate ?? string.Empty;
        Priority = priority;
        Status = status;
        ReplaceItems(items);
    }

    public int Id { get; private set; }

    public string Code { get; private set; }

    public int SupplierId { get; private set; }

    public string SupplierName { get; private set; }

    public string RestaurantName { get; private set; }

    public string OrderDate { get; private set; }

    public string EstimatedDate { get; private set; }

    public EPurchaseOrderPriority Priority { get; private set; }

    public EPurchaseOrderStatus Status { get; private set; }

    public ICollection<PurchaseOrderItem> Items { get; private set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public void Update(
        string code,
        int supplierId,
        string supplierName,
        string restaurantName,
        string orderDate,
        string? estimatedDate,
        EPurchaseOrderPriority priority,
        EPurchaseOrderStatus status,
        IEnumerable<PurchaseOrderItem> items)
    {
        Code = code;
        SupplierId = supplierId;
        SupplierName = supplierName;
        RestaurantName = restaurantName;
        OrderDate = orderDate;
        EstimatedDate = estimatedDate ?? string.Empty;
        Priority = priority;
        Status = status;
        ReplaceItems(items);
    }

    public bool CanTransitionTo(EPurchaseOrderStatus nextStatus)
    {
        if (Status == nextStatus) return true;
        if (Status == EPurchaseOrderStatus.Delivered) return false;
        if (nextStatus == EPurchaseOrderStatus.Delayed) return Status is EPurchaseOrderStatus.Pending or EPurchaseOrderStatus.Confirmed or EPurchaseOrderStatus.InTransit;

        return Status switch
        {
            EPurchaseOrderStatus.Pending => nextStatus == EPurchaseOrderStatus.Confirmed,
            EPurchaseOrderStatus.Confirmed => nextStatus == EPurchaseOrderStatus.InTransit,
            EPurchaseOrderStatus.InTransit => nextStatus == EPurchaseOrderStatus.Delivered,
            EPurchaseOrderStatus.Delayed => false,
            _ => false
        };
    }

    public void UpdateStatus(EPurchaseOrderStatus nextStatus)
    {
        if (!CanTransitionTo(nextStatus))
            throw new ArgumentException($"Invalid purchase order status transition from {Status} to {nextStatus}.");

        Status = nextStatus;
    }

    private void ReplaceItems(IEnumerable<PurchaseOrderItem> items)
    {
        Items.Clear();
        foreach (var item in items) Items.Add(item);
    }
}
