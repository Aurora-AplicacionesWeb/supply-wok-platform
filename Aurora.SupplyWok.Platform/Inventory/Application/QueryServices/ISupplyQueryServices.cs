using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Queries;
namespace Aurora.SupplyWok.Platform.Inventory.Application.QueryServices;

/// <summary>
/// Supply query services
/// </summary>
public interface ISupplyQueryServices
{
    Task<IEnumerable<Supply>> Handle(GetAllSuppliesQuery query, CancellationToken cancellationToken);
    Task<Supply?> Handle(GetSupplyByIdQuery query, CancellationToken cancellationToken);
    Task<int> Handle(GetTotalSupplyStockQuery query, CancellationToken cancellationToken);
}
