using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Queries;

namespace Aurora.SupplyWok.Platform.Inventory.Application.QueryServices;

/// <summary>
/// Stock movement query service interface.
/// </summary>
public interface IStockMovementQueryService
{
    Task<IEnumerable<StockMovement>> Handle(GetAllStockMovementsQuery query, CancellationToken cancellationToken);
    Task<StockMovement?> Handle(GetStockMovementByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<StockMovement>> Handle(GetStockMovementsBySupplyIdQuery query, CancellationToken cancellationToken);
}
