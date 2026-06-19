using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Inventory.Domain.Model.Aggregate;

/// <summary>
/// Represents the Supply aggregate in the Supply Wok Platform
/// </summary>
public partial class Supply
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Supply"/> aggregate.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="unitOfMeasure"></param>
    /// <param name="currentStock"></param>
    /// <param name="minimumStockLevel"></param>
    /// <param name="category"></param>
    public Supply(string name, EUnitOfMeasure unitOfMeasure, int currentStock, int minimumStockLevel,
        string category) : this()
    {
        Name = name;
        UnitOfMeasure = unitOfMeasure;
        CurrentStock = currentStock;
        MinimumStockLevel = minimumStockLevel;
        Category = category;
    }

    public Supply(CreateSupplyCommand command) : this(command.Name, command.UnitOfMeasure, command.CurrentStock,
        command.MinimumStockLevel, command.Category)
    {
        
    }
    
    public int Id { get; }
    public string Name { get; private set; }
    public EUnitOfMeasure UnitOfMeasure { get; private set; }
    public int CurrentStock { get; private set; }
    public int MinimumStockLevel { get; private set; }
    public string Category { get; private set; }

    public void Update(string name, EUnitOfMeasure unitOfMeasure, int minimumStockLevel, string category)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Supply name cannot be empty.", nameof(name));
        if (minimumStockLevel < 0)
            throw new ArgumentException("Minimum stock level cannot be negative.", nameof(minimumStockLevel));
        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentException("Supply category cannot be empty.", nameof(category));
        
        Name = name;
        UnitOfMeasure = unitOfMeasure;
        MinimumStockLevel = minimumStockLevel;
        Category = category;
    }

    public void IncreaseStock(int amount)
    {
        ValidateMovementAmount(amount);
        CurrentStock += amount;
    }

    public void DecreaseStock(int amount)
    {
        ValidateMovementAmount(amount);
        if (CurrentStock - amount < 0)
            throw new InvalidOperationException("Current stock cannot be negative.");

        CurrentStock -= amount;
    }

    private static void ValidateMovementAmount(int amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Movement amount must be greater than zero.", nameof(amount));
    }
}
