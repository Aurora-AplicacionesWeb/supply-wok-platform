using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Shared.Application.Model;

namespace Aurora.SupplyWok.Platform.Inventory.Application.CommandServices;

/// <summary>
/// Stock movement command service interface.
/// </summary>
public interface IStockMovementCommandService
{
    Task<Result<StockMovement>> Handle(CreateStockMovementCommand command, CancellationToken cancellationToken);
}
