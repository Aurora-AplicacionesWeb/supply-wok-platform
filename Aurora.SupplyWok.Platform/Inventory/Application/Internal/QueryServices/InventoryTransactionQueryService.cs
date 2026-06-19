using Aurora.SupplyWok.Platform.Inventory.Application.QueryServices;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Inventory.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Inventory.Application.Internal.QueryServices;

public class InventoryTransactionQueryService(
    IInventoryTransactionRepository inventoryTransactionRepository) : IInventoryTransactionQueryService
{
    public async Task<IEnumerable<InventoryTransaction>> Handle(GetAllInventoryTransactionsQuery query, CancellationToken cancellationToken)
    {
        return await inventoryTransactionRepository.ListAsync(cancellationToken);
    }

    public async Task<InventoryTransaction?> Handle(GetInventoryTransactionByIdQuery query, CancellationToken cancellationToken)
    {
        return await inventoryTransactionRepository.FindWithOperationsByIdAsync(query.InventoryTransactionId, cancellationToken);
    }

    public async Task<IEnumerable<InventoryTransaction>> Handle(GetInventoryTransactionsBySupplyIdQuery query, CancellationToken cancellationToken)
    {
        return await inventoryTransactionRepository.FindBySupplyIdAsync(query.SupplyId, cancellationToken);
    }
}
