namespace Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;

/// <summary>
/// Partial content class for the <see cref="SupplierReference"/> aggregate.
/// </summary>
public partial class SupplierReference
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SupplierReference"/> aggregate with default values.
    /// </summary>
    public SupplierReference()
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
