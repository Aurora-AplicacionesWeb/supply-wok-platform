namespace Aurora.SupplyWok.Platform.Operations.Domain.Model.Entities;

public class KitchenOrderItem
{
    public KitchenOrderItem()
    {
        DishName = string.Empty;
        Code = string.Empty;
        Description = string.Empty;
    }

    public KitchenOrderItem(int dishId, string dishName, int quantity, double unitPrice,
        string code, string description, int dishCategoryId, bool active, bool outstanding) : this()
    {
        DishId = dishId;
        DishName = dishName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Code = code;
        Description = description;
        DishCategoryId = dishCategoryId;
        Active = active;
        Outstanding = outstanding;
    }

    public int Id { get; private set; }
    public int KitchenOrderId { get; private set; }
    public int DishId { get; private set; }
    public string DishName { get; private set; }
    public int Quantity { get; private set; }
    public double UnitPrice { get; private set; }
    public double SubTotal => Quantity * UnitPrice;
    public string Code { get; private set; }
    public string Description { get; private set; }
    public int DishCategoryId { get; private set; }
    public bool Active { get; private set; }
    public bool Outstanding { get; private set; }

    public void UpdateQuantity(int quantity)
    {
        Quantity = quantity;
    }
}
