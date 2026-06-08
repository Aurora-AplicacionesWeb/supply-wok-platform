using Aurora.SupplyWok.Platform.Operations.Domain.Model.ValueObjects;
namespace Aurora.SupplyWok.Platform.Operations.Domain.Model.Aggregate;

/// <summary>
/// Partial class for the <see cref="Table"/> aggregate
/// </summary>
public partial class Table{

    /// <summary>
    /// Initializes a new instance of the <see cref="Table"/> class with default values.
    /// </summary>
    public Table()
    {
        Number = string.Empty;
        Capacity = 0;
        Location = string.Empty;
        State = ETableStatus.Available;
        Active = true;
    }
}