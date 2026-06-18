namespace Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Aggregates;

/// <summary>
///     Supplier-client link aggregate for the Suppliers bounded context.
/// </summary>
/// <remarks>
///     This aggregate represents the explicit relationship between a supplier and a client.
/// </remarks>
public partial class SupplierClient
{
    /// <summary>
    ///     Initializes a new supplier-client link instance with business data.
    /// </summary>
    /// <param name="supplierId">The supplier identifier.</param>
    /// <param name="clientId">The client identifier.</param>
    public SupplierClient(int supplierId, int clientId)
    {
        SupplierId = supplierId;
        ClientId = clientId;
    }

    /// <summary>
    ///     Gets the supplier-client link identifier.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    ///     Gets the related supplier identifier.
    /// </summary>
    public int SupplierId { get; private set; }

    /// <summary>
    ///     Gets the related client identifier.
    /// </summary>
    public int ClientId { get; private set; }
}
