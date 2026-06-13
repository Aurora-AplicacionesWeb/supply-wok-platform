using Aurora.SupplyWok.Platform.Inventory.Application.QueryServices;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Inventory.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Inventory.Application.Internal.QueryServices;

public class StockMovementQueryService(IStockMovementRepository stockMovementRepository) : IStockMovementQueryService
{
    public async Task<IEnumerable<StockMovement>> Handle(GetAllStockMovementsQuery query, CancellationToken cancellationToken)
    {
        return await stockMovementRepository.ListAsync(cancellationToken);
    }

    public async Task<StockMovement?> Handle(GetStockMovementByIdQuery query, CancellationToken cancellationToken)
    {
        return await stockMovementRepository.GetStockMovementByIdAsync(query.StockMovementId, cancellationToken);
    }

    public async Task<IEnumerable<StockMovement>> Handle(
        GetStockMovementsBySupplyIdQuery query,
        CancellationToken cancellationToken)
    {
        return await stockMovementRepository.ListBySupplyIdAsync(query.SupplyId, cancellationToken);
    }
}
