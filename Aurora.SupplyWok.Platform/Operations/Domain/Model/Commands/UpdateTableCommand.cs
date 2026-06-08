using Aurora.SupplyWok.Platform.Operations.Domain.Model.ValueObjects;
namespace Aurora.SupplyWok.Platform.Operations.Domain.Model.Commands;

/// <summary>
/// Command to update an existing table.
/// </summary>
/// <param name="Id">
/// The unique identifier of the table to update. This is required to identify which table record should be updated in the database.
/// </param>
/// <param name="Number">
/// The number of the table to update. This should be a separate identifier from the ID of the table.
/// </param>
/// <param name="Capacity">
/// The people capacity of the table. This indicates how many people can be seated at the table.
/// </param>
/// <param name="Location">
/// The location of the table. This could be a description of where the table is located within the restaurant, such as "Main Hall" or "Terrace".
/// </param>
/// <param name="State">
/// The state of the table. This could be an enumeration that indicates whether the table is "Available" or "Busy".
/// </param>
/// <param name="Active">
/// Indicates whether the table is active. This could be permanently set to true for all tables, or it could be used to indicate whether the table is currently in use or not. For example, if a table is under maintenance, it could be marked as inactive.
/// </param>
public record UpdateTableCommand(int Id, string Number, int Capacity, string Location, ETableStatus State, bool Active);