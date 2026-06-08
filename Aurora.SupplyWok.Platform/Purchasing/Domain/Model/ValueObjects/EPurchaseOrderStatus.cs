namespace Aurora.SupplyWok.Platform.Purchasing.Domain.Model.ValueObjects;

/// <summary>
/// Represents the current status of a purchase order.
/// </summary>
public enum EPurchaseOrderStatus
{
    Pending,
    Confirmed,
    InTransit,
    Delivered,
    Delayed
}
