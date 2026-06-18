namespace Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Aggregates;

/// <summary>
/// Partial content class for the <see cref="Client"/> aggregate.
/// </summary>
public partial class Client
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Client"/> aggregate with default values.
    /// </summary>
    public Client()
    {
        Name = string.Empty;
        District = string.Empty;
        Status = string.Empty;
    }
}
