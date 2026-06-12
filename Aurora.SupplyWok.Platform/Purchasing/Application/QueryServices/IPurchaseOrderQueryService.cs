using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Queries;

namespace Aurora.SupplyWok.Platform.Purchasing.Application.QueryServices;

public interface IPurchaseOrderQueryService
{
    Task<IEnumerable<PurchaseOrder>> Handle(GetAllPurchaseOrdersQuery query, CancellationToken cancellationToken);

    Task<IEnumerable<PurchaseOrder>> Handle(GetPurchaseOrdersBySupplierIdQuery query, CancellationToken cancellationToken);

    Task<PurchaseOrder?> Handle(GetPurchaseOrderByIdQuery query, CancellationToken cancellationToken);
}
