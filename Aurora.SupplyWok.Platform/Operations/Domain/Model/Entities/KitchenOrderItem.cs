namespace Aurora.SupplyWok.Platform.Operations.Domain.Model.Entities;

public class KitchenOrderItem
{
    public KitchenOrderItem()
    {
        DishName = string.Empty;
    }

    public KitchenOrderItem(int dishId, string dishName, int quantity, double unitPrice) : this()
    {
        DishId = dishId;
        DishName = dishName;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public int Id { get; }
    public int KitchenOrderId { get; }
    public int DishId { get; private set; }
    public string DishName { get; private set; }
    public int Quantity { get; private set; }
    public double UnitPrice { get; private set; }
    public double SubTotal => Quantity * UnitPrice;

    public void UpdateQuantity(int quantity)
    {
        Quantity = quantity;
    }
}
