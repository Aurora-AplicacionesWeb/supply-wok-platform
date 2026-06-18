namespace Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Aggregates;

/// <summary>
/// Partial content class for the <see cref="Supplier"/> aggregate.
/// </summary>
public partial class Supplier
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Supplier"/> aggregate with default values.
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
}
