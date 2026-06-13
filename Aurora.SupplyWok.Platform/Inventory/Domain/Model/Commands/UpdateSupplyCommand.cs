using Aurora.SupplyWok.Platform.Inventory.Domain.Model.ValueObjects;
namespace Aurora.SupplyWok.Platform.Inventory.Domain.Model.Commands;

/// <summary>
/// Command to update a supply item.
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="UnitOfMeasure"></param>
/// <param name="MinimumStockLevel"></param>
/// <param name="category"></param>
public record UpdateSupplyCommand(int Id, string Name, EUnitOfMeasure UnitOfMeasure, int MinimumStockLevel, string category);
