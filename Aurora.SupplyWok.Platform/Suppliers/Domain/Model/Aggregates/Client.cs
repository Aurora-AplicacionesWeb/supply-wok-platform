namespace Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Aggregates;

/// <summary>
///     Client aggregate owned by the Suppliers bounded context.
/// </summary>
/// <remarks>
///     In this bounded context, a client represents a restaurant known by the supplier.
/// </remarks>
public partial class Client
{
    /// <summary>
    ///     Initializes a new client instance with business data.
    /// </summary>
    /// <param name="name">The restaurant display name.</param>
    /// <param name="district">The district where the restaurant operates.</param>
    /// <param name="status">The current supplier-side status for the client.</param>
    public Client(
        string name,
        string district,
        string status) : this()
    {
        Name = name;
        District = district;
        Status = status;
    }

    /// <summary>
    ///     Gets the client identifier.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    ///     Gets the restaurant display name.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    ///     Gets the district associated with the client.
    /// </summary>
    public string District { get; private set; }

    /// <summary>
    ///     Gets the current status of the client in the supplier workspace.
    /// </summary>
    public string Status { get; private set; }
}
