using Aurora.SupplyWok.Platform.Purchasing.Application.QueryServices;
using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Purchasing.Interfaces.Acl;

namespace Aurora.SupplyWok.Platform.Purchasing.Application.Acl;

/// <summary>
/// Application facade for supplier operations exposed through the ACL.
/// </summary>
public class SupplierContextFacade(IPurchaseOrderQueryService purchaseOrderQueryService) : ISupplierContextFacade
{
    /// <inheritdoc />
    public async Task<decimal> CalculateSupplierSla(int supplierId, CancellationToken cancellationToken)
    {
        var purchaseOrders = await purchaseOrderQueryService.Handle(new GetAllPurchaseOrdersQuery(), cancellationToken);
        var sla = new PurchaseOrderSla(supplierId, purchaseOrders);
        return sla.ComplianceRate;
    }
}
