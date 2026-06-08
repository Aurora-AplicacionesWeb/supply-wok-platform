using Aurora.SupplyWok.Platform.Shared.Domain.Model.Entities;
namespace Aurora.SupplyWok.Platform.Operations.Domain.Model.Aggregate;

public partial class Table : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }   
}