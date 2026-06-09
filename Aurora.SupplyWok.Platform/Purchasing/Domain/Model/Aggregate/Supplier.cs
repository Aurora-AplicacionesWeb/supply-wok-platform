namespace Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Aggregate;

/// <summary>
/// Supplier aggregate required by purchase orders and SLA calculations.
/// </summary>
public partial class Supplier
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Supplier"/> aggregate.
    /// </summary>
    public Supplier()
    {
        Uuid = Guid.Empty;
        Name = string.Empty;
        ContactName = string.Empty;
        Email = string.Empty;
        Phone = string.Empty;
        Category = string.Empty;
        LinkedDate = string.Empty;
        Sla = string.Empty;
        ResponseTime = string.Empty;
    }

    /// <summary>
    /// Gets the numeric supplier identifier used by the current frontend contract.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Gets the supplier UUID used as a stable cross-context identity.
    /// </summary>
    public Guid Uuid { get; private set; }

    /// <summary>
    /// Gets the supplier display name.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the supplier contact name.
    /// </summary>
    public string ContactName { get; private set; }

    /// <summary>
    /// Gets the supplier contact email.
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Gets the supplier contact phone.
    /// </summary>
    public string Phone { get; private set; }

    /// <summary>
    /// Gets the supplier category.
    /// </summary>
    public string Category { get; private set; }

    /// <summary>
    /// Gets the supplier linked date in yyyy-MM-dd format.
    /// </summary>
    public string LinkedDate { get; private set; }

    /// <summary>
    /// Gets the supplier SLA display value.
    /// </summary>
    public string Sla { get; private set; }

    /// <summary>
    /// Gets the supplier response time display value.
    /// </summary>
    public string ResponseTime { get; private set; }
}
