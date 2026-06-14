using Aurora.SupplyWok.Platform.Inventory.Application.QueryServices;

namespace Aurora.SupplyWok.Platform.Inventory.Domain.Model.Queries;

public record GetTotalSupplyStockQuery
{
    public async Task<int> Handle(ISupplyQueryServices supplyQueryServices, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
