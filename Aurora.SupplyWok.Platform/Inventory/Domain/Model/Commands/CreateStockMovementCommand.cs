using Aurora.SupplyWok.Platform.Inventory.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Inventory.Domain.Model.Commands;

/// <summary>
/// Command to create a stock movement.
/// </summary>
/// <param name="SupplyId">The supply identifier.</param>
/// <param name="Type">The movement type.</param>
/// <param name="Amount">The movement amount.</param>
/// <param name="Date">The movement date.</param>
/// <param name="Reason">The movement reason.</param>
public record CreateStockMovementCommand(int SupplyId, EMovementType Type, int Amount, DateTime Date, string Reason);
