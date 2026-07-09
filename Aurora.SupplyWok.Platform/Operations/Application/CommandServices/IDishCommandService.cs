using Aurora.SupplyWok.Platform.Operations.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Shared.Application.Model;

namespace Aurora.SupplyWok.Platform.Operations.Application.CommandServices;

public interface IDishCommandService
{
    Task<Result<Dish>> Handle(CreateDishCommand command, CancellationToken cancellationToken);
}
