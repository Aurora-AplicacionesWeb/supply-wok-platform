using Aurora.SupplyWok.Platform.Shared.Domain.Model.Entities;

namespace Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;

public partial class SupplierSettings : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}
