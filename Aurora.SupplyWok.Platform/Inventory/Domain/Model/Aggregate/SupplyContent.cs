using Aurora.SupplyWok.Platform.Inventory.Domain.Model.ValueObjects;
namespace Aurora.SupplyWok.Platform.Inventory.Domain.Model.Aggregate;

/// <summary>
/// Partial class for the <see cref="Supply"/> aggregate
/// </summary>
public partial class Supply
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Supply"/> class with default values.
    /// </summary>
    public Supply()
    {
        Name = string.Empty;
        UnitOfMeasure = EUnitOfMeasure.Units;
        CurrentStock = 0;
        MinimumStockLevel = 0;
        Category = string.Empty;
    }
}
