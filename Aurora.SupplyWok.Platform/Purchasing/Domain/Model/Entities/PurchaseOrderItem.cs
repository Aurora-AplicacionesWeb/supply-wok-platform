namespace Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Entities;

/// <summary>
/// Represents a purchase order line item entity.
/// </summary>
public class PurchaseOrderItem
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PurchaseOrderItem"/> entity with default values.
    /// </summary>
    public PurchaseOrderItem()
    {
        ProductName = string.Empty;
        UnitType = string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PurchaseOrderItem"/> entity.
    /// </summary>
    /// <param name="inventoryItemId">The optional inventory item identifier.</param>
    /// <param name="productName">The purchased product name.</param>
    /// <param name="quantity">The purchased quantity.</param>
    /// <param name="unitPrice">The unit price.</param>
    /// <param name="unitType">The unit type.</param>
    public PurchaseOrderItem(int? inventoryItemId, string productName, decimal quantity, decimal unitPrice, string unitType)
    {
        InventoryItemId = inventoryItemId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        UnitType = unitType;
    }

    /// <summary>
    /// Gets the line item identifier.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Gets the purchase order identifier that owns this line item.
    /// </summary>
    public int PurchaseOrderId { get; private set; }

    /// <summary>
    /// Gets the optional inventory item identifier.
    /// </summary>
    public int? InventoryItemId { get; private set; }

    /// <summary>
    /// Gets the purchased product name.
    /// </summary>
    public string ProductName { get; private set; }

    /// <summary>
    /// Gets the purchased quantity.
    /// </summary>
    public decimal Quantity { get; private set; }

    /// <summary>
    /// Gets the unit price.
    /// </summary>
    public decimal UnitPrice { get; private set; }

    /// <summary>
    /// Gets the unit type.
    /// </summary>
    public string UnitType { get; private set; }
}
