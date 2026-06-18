using Aurora.SupplyWok.Platform.Operations.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Shared.Application.Model;

namespace Aurora.SupplyWok.Platform.Operations.Application.CommandServices;

public interface IKitchenOrderCommandService
{
    Task<Result<KitchenOrder>> Handle(CreateKitchenOrderCommand command, CancellationToken cancellationToken);
    Task<Result<KitchenOrder>> Handle(UpdateKitchenOrderCommand command, CancellationToken cancellationToken);
    Task<Result<bool>> Handle(DeleteKitchenOrderCommand command, CancellationToken cancellationToken);
    Task<Result<KitchenOrder>> Handle(UpdateKitchenOrderStatusCommand command, CancellationToken cancellationToken);
    Task<Result<KitchenOrder>> Handle(AddDishToKitchenOrderCommand command, CancellationToken cancellationToken);
}
