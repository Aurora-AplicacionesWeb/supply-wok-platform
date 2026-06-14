using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Shared.Application.Model;
namespace Aurora.SupplyWok.Platform.Inventory.Application.CommandServices;

/// <summary>
/// Supply command service interface
/// </summary>
public interface ISupplyCommandService
{
    Task<Result<Supply>> Handle(CreateSupplyCommand command, CancellationToken cancellationToken);
    Task<Result<Supply>> Handle(UpdateSupplyCommand command, CancellationToken cancellationToken);
    Task<Result<bool>> Handle(DeleteSupplyCommand command, CancellationToken cancellationToken);
}