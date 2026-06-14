namespace Aurora.SupplyWok.Platform.Inventory.Domain.Model.Commands;

/// <summary>
/// Command to delete a supply item.
/// </summary>
/// <param name="Id">
/// The unique identifier of the supply item to delete.
/// </param>
public record DeleteSupplyCommand(int Id);