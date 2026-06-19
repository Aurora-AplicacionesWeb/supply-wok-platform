namespace Aurora.SupplyWok.Platform.Profiles.Domain.Model.Aggregates;

/// <summary>
/// Partial class for the <see cref="SupplierProfile"/> aggregate
/// </summary>
public partial class SupplierProfile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SupplierProfile"/> class with default values.
    /// </summary>
    public SupplierProfile()
    {
        BusinessName = string.Empty;
        Status = "Active";
    }
}
