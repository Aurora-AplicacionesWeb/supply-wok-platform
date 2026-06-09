using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.ValueObjects;
using Aurora.SupplyWok.Platform.Shared.Domain.Model.Entities;

namespace Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Entities;

/// <summary>
/// Represents a purchase order entity in the Supply Wok purchasing flow.
/// </summary>
public class PurchaseOrder : IAuditableEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PurchaseOrder"/> entity with default values.
    /// </summary>
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

    /// <summary>
    /// Initializes a new instance of the <see cref="PurchaseOrder"/> entity with business data.
    /// </summary>
    /// <param name="code">The purchase order code.</param>
    /// <param name="supplierId">The supplier identifier.</param>
    /// <param name="supplierName">The supplier display name.</param>
    /// <param name="restaurantName">The restaurant display name.</param>
    /// <param name="orderDate">The order date in yyyy-MM-dd format.</param>
    /// <param name="estimatedDate">The estimated delivery date in yyyy-MM-dd format.</param>
    /// <param name="priority">The purchase order priority.</param>
    /// <param name="status">The purchase order status.</param>
    /// <param name="items">The purchase order line items.</param>
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

    /// <summary>
    /// Gets the purchase order identifier.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Gets the purchase order code.
    /// </summary>
    public string Code { get; private set; }

    /// <summary>
    /// Gets the supplier identifier.
    /// </summary>
    public int SupplierId { get; private set; }

    /// <summary>
    /// Gets the supplier display name.
    /// </summary>
    public string SupplierName { get; private set; }

    /// <summary>
    /// Gets the restaurant display name.
    /// </summary>
    public string RestaurantName { get; private set; }

    /// <summary>
    /// Gets the order date in yyyy-MM-dd format.
    /// </summary>
    public string OrderDate { get; private set; }

    /// <summary>
    /// Gets the estimated delivery date in yyyy-MM-dd format.
    /// </summary>
    public string EstimatedDate { get; private set; }

    /// <summary>
    /// Gets the purchase order priority.
    /// </summary>
    public EPurchaseOrderPriority Priority { get; private set; }

    /// <summary>
    /// Gets the purchase order status.
    /// </summary>
    public EPurchaseOrderStatus Status { get; private set; }

    /// <summary>
    /// Gets the purchase order line items.
    /// </summary>
    public ICollection<PurchaseOrderItem> Items { get; private set; }

    /// <inheritdoc />
    public DateTimeOffset? CreatedAt { get; set; }

    /// <inheritdoc />
    public DateTimeOffset? UpdatedAt { get; set; }

    /// <summary>
    /// Updates the purchase order data and replaces its line items.
    /// </summary>
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

    /// <summary>
    /// Determines whether the current status can transition to the requested status.
    /// </summary>
    /// <param name="nextStatus">The requested next status.</param>
    /// <returns>True when the transition is allowed; otherwise false.</returns>
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

    /// <summary>
    /// Updates the purchase order status after checking transition rules.
    /// </summary>
    /// <param name="nextStatus">The requested next status.</param>
    /// <exception cref="ArgumentException">Thrown when the transition is invalid.</exception>
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
