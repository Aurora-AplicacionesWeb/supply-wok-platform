using Aurora.SupplyWok.Platform.Shared.Domain.Model.Entities;

namespace Aurora.SupplyWok.Platform.Profiles.Domain.Model.Aggregates;

public partial class SupplierProfile : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}