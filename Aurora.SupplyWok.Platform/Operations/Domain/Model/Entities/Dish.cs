using Aurora.SupplyWok.Platform.Operations.Domain.Model.Entities;
namespace Aurora.SupplyWok.Platform.Operations.Domain.Model.Entities;

public class Dish
{
    public Dish()
    {
        Code = string.Empty;
        Name = string.Empty;
        Quantity = 0;
        Description = string.Empty;
        Price = 0.0;
        Active = true;
        Outstanding = true;
        DishCategoryId = new DishCategory().Id;
    }

    public Dish(string code, string name, int quantity, string description, double price, bool active, bool outstanding, int dishCategoryId) : this()
    {
        Code = code;
        Name = name;
        Quantity = quantity;
        Description = description;
        Price = price;
        Active = active;
        Outstanding = outstanding;
        DishCategoryId = dishCategoryId;
    }
    
    public int Id { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public int Quantity { get; private set; }
    public string Description { get; private set; }
    public double Price { get; private set; }
    public bool Active { get; private set; }
    public bool Outstanding { get; private set; }
    public int DishCategoryId { get; private set; }
}