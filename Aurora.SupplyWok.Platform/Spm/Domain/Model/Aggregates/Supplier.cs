namespace Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;

/// <summary>
/// Supplier aggregate owned by the Suppliers bounded context.
/// </summary>
public partial class Supplier
{
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
