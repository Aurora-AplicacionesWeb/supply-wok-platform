using Aurora.SupplyWok.Platform.Operations.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Operations.Domain.Model.Aggregate;

/// <summary>
/// Represents the Table aggregate in the Supply Wok Platform
/// </summary>
public partial class Table
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Table"/> aggregate.
    /// </summary>
    /// <param name="number">The number of the table.</param>
    /// <param name="capacity">The capacity of the table.</param>
    /// <param name="location">The location of the table.</param>
    /// <param name="state">The status of the table.</param>
    /// <param name="active">Indicates if the table is active.</param>
    public Table(string number, int capacity, string location, ETableStatus state, bool active) : this()
    {
        Number = number;
        Capacity = capacity;
        Location = location;
        State = state;
        Active = active;
    }

    public Table(CreateTableCommand command): this(command.Number, command.Capacity, command.Location, command.State,
        command.Active)
    {
    }
    
    public int Id { get; }
    public string Number { get; private set; }
    public int Capacity { get; private set; }
    public string Location { get; private set; }
    public ETableStatus State { get; private set; }
    public bool Active { get; private set; }
    
    public void Update(string number, int capacity, string location, ETableStatus state, bool active)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Table number cannot be empty.", nameof(number));
        if (capacity <= 0)
            throw new ArgumentException("Table capacity must be greater than zero.", nameof(capacity));
        if (string.IsNullOrWhiteSpace(location))
            throw new ArgumentException("Table location cannot be empty.", nameof(location));

        Number = number;
        Capacity = capacity;
        Location = location;
        State = state;
        Active = active;
    }
}