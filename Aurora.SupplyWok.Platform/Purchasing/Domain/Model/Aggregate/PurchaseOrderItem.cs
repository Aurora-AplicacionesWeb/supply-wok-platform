namespace Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Aggregate;

/// <summary>
/// Represents a line item in a purchase order.
/// </summary>
public class PurchaseOrderItem
{
    public PurchaseOrderItem()
    {
        ProductName = string.Empty;
        UnitType = string.Empty;
    }

    public PurchaseOrderItem(int? inventoryItemId, string productName, decimal quantity, decimal unitPrice, string unitType)
    {
        InventoryItemId = inventoryItemId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        UnitType = unitType;
    }

    public int Id { get; private set; }

    public int PurchaseOrderId { get; private set; }

    public int? InventoryItemId { get; private set; }

    public string ProductName { get; private set; }

    public decimal Quantity { get; private set; }

    public decimal UnitPrice { get; private set; }

    public string UnitType { get; private set; }
}
