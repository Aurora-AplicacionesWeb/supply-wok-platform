using Aurora.SupplyWok.Platform.Inventory.Domain.Model.ValueObjects;
namespace Aurora.SupplyWok.Platform.Inventory.Domain.Model.Commands;

/// <summary>
/// Command to create a new supply
/// </summary>
/// <param name="Name"></param>
/// <param name="UnitOfMeasure"></param>
/// <param name="CurrentStock"></param>
/// <param name="MinimumStockLevel"></param>
/// <param name="category"></param>
public record CreateSupplyCommand(string Name, EUnitOfMeasure UnitOfMeasure, int CurrentStock, int MinimumStockLevel, string category);