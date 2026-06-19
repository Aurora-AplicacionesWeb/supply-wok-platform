using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Shared.Application.Model;

namespace Aurora.SupplyWok.Platform.Inventory.Application.CommandServices;

/// <summary>
/// Inventory transaction command service interface.
/// </summary>
public interface IInventoryTransactionCommandService
{
    Task<Result<InventoryTransaction>> Handle(CreateInventoryTransactionCommand command, CancellationToken cancellationToken);
}
