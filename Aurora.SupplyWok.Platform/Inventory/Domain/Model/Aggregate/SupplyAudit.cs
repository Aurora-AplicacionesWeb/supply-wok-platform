using Aurora.SupplyWok.Platform.Shared.Domain.Model.Entities;
namespace Aurora.SupplyWok.Platform.Inventory.Domain.Model.Aggregate;

public partial class Supply : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }  
}
