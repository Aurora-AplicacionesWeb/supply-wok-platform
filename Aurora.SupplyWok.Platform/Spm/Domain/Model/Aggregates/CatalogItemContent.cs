using Aurora.SupplyWok.Platform.Spm.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;

/// <summary>
/// Partial content class for the <see cref="CatalogItem"/> aggregate.
/// </summary>
public partial class CatalogItem
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CatalogItem"/> aggregate with default values.
    /// </summary>
    public CatalogItem()
    {
        Name = string.Empty;
        Category = string.Empty;
        DeliveryConditions = string.Empty;
        Unit = ECatalogUnit.Kg;
    }
}
