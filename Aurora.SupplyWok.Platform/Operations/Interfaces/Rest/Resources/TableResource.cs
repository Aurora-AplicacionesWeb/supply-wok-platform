using Aurora.SupplyWok.Platform.Operations.Domain.Model.ValueObjects;
namespace Aurora.SupplyWok.Platform.Operations.Interfaces.Rest.Resources;

/// <summary>
/// Table resource for REST API
/// </summary>
/// <param name="Id">
/// The unique identifier of the table. This is an integer value then automatically generated when a new table is created. It serves as the primary key for the table resource and is used to uniquely identify each table in the system.
/// </param>
/// <param name="Number">
/// The number of the table. This is a string value that uniquely identifies the table within the system.
/// </param>
/// <param name="Capacity">
/// The capacity of the table. This is an integer value that indicates the maximum number of people that can be seated at the table.
/// </param>
/// <param name="Location">
/// The location of the table. This is a string value that indicates where the table is located within the restaurant.
/// </param>
/// <param name="State">
/// The state of the table. This is an enumeration value that indicates the current status of the table (e.g., available, busy).
/// </param>
/// <param name="Active">
/// Indicates whether the table is active. This is a boolean value that determines if the table can be used for reservations or seating.
/// </param>
public record TableResource(int Id, string Number, int Capacity, string Location, ETableStatus State, bool Active);