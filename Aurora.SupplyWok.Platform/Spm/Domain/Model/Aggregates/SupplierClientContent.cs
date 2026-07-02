namespace Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;

/// <summary>
/// Partial content class for the <see cref="SupplierRestaurant"/> aggregate.
/// </summary>
public partial class SupplierRestaurant
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SupplierRestaurant"/> aggregate with default values.
    /// </summary>
    public SupplierRestaurant()
    {
        LinkedDate = string.Empty;
        Status = string.Empty;
        Sla = string.Empty;
        ResponseTime = string.Empty;
    }
}
