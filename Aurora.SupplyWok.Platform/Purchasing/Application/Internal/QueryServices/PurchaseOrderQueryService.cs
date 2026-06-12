using Aurora.SupplyWok.Platform.Purchasing.Application.QueryServices;
using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Purchasing.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Purchasing.Application.Internal.QueryServices;

public class PurchaseOrderQueryService(IPurchaseOrderRepository purchaseOrderRepository) : IPurchaseOrderQueryService
{
    public async Task<IEnumerable<PurchaseOrder>> Handle(GetAllPurchaseOrdersQuery query, CancellationToken cancellationToken)
    {
        return await purchaseOrderRepository.ListPurchaseOrdersAsync(cancellationToken);
    }

    public async Task<IEnumerable<PurchaseOrder>> Handle(GetPurchaseOrdersBySupplierIdQuery query, CancellationToken cancellationToken)
    {
        return await purchaseOrderRepository.ListPurchaseOrdersBySupplierIdAsync(query.SupplierId, cancellationToken);
    }

    public async Task<PurchaseOrder?> Handle(GetPurchaseOrderByIdQuery query, CancellationToken cancellationToken)
    {
        return await purchaseOrderRepository.GetPurchaseOrderByIdAsync(query.PurchaseOrderId, cancellationToken);
    }
}
