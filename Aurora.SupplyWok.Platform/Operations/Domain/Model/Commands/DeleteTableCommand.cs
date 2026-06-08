namespace Aurora.SupplyWok.Platform.Operations.Domain.Model.Commands;

/// <summary>
/// Delete a table
/// </summary>
/// <param name="Id">
/// The unique identifier of the table to delete.
/// </param>
public record DeleteTableCommand(int Id);