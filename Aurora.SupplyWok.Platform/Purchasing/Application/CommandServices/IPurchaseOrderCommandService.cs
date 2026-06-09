using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Shared.Application.Model;

namespace Aurora.SupplyWok.Platform.Purchasing.Application.CommandServices;

public interface IPurchaseOrderCommandService
{
    Task<Result<PurchaseOrder>> Handle(CreatePurchaseOrderCommand command, CancellationToken cancellationToken);

    Task<Result<PurchaseOrder>> Handle(UpdatePurchaseOrderCommand command, CancellationToken cancellationToken);

    Task<Result<bool>> Handle(DeletePurchaseOrderCommand command, CancellationToken cancellationToken);

    Task<Result<PurchaseOrder>> Handle(UpdatePurchaseOrderStatusCommand command, CancellationToken cancellationToken);
}
