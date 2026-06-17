namespace Aurora.SupplyWok.Platform.Operations.Domain.Model.Entities;

public class DishCategory
{
    public DishCategory()
    {
        Name = string.Empty;
        Order = 0;
        Active = true;
    }

    public DishCategory(string name, int order, bool active) : this()
    {
        Name = name;
        Order = order;
        Active = active;
    }
    
    public int Id { get; private set; }
    public string Name { get; private set; }
    public int Order { get; private set; }
    public bool Active { get; private set; }   
}