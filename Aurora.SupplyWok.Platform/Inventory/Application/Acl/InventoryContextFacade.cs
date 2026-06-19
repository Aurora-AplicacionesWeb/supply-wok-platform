using Aurora.SupplyWok.Platform.Inventory.Application.QueryServices;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Inventory.Interfaces.Acl;

namespace Aurora.SupplyWok.Platform.Inventory.Application.Acl;

public class InventoryContextFacade(ISupplyQueryServices supplyQueryServices) : IInventoryContextFacade
{
    public async Task<int> GetTotalSupplyStockAsync(CancellationToken cancellationToken)
    {
        var query = new GetTotalSupplyStockQuery();
        return await supplyQueryServices.Handle(query, cancellationToken);
    }
}
