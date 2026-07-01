using Aurora.SupplyWok.Platform.Shared.Domain.Model.Entities;

namespace Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;

/// <summary>
///     Audit metadata for the <see cref="CatalogItem"/> aggregate.
/// </summary>
public partial class CatalogItem : IAuditableEntity
{
    /// <inheritdoc />
    public DateTimeOffset? CreatedAt { get; set; }

    /// <inheritdoc />
    public DateTimeOffset? UpdatedAt { get; set; }
}
