using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Queries;

namespace Aurora.SupplyWok.Platform.Inventory.Application.QueryServices;

/// <summary>
/// Inventory transaction query service interface.
/// </summary>
public interface IInventoryTransactionQueryService
{
    Task<IEnumerable<InventoryTransaction>> Handle(GetAllInventoryTransactionsQuery query, CancellationToken cancellationToken);
    Task<InventoryTransaction?> Handle(GetInventoryTransactionByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<InventoryTransaction>> Handle(GetInventoryTransactionsBySupplyIdQuery query, CancellationToken cancellationToken);
}
