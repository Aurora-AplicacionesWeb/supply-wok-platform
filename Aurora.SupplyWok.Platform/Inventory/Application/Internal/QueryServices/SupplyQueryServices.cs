using Aurora.SupplyWok.Platform.Inventory.Application.QueryServices;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Inventory.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Inventory.Application.Internal.QueryServices;

public class SupplyQueryServices(ISupplyRepository supplyRepository) : ISupplyQueryServices
{
    public async Task<IEnumerable<Supply>> Handle(GetAllSuppliesQuery query, CancellationToken cancellationToken)
    {
        return await supplyRepository.ListAsync(cancellationToken);
    }

    public async Task<Supply?> Handle(GetSupplyByIdQuery query, CancellationToken cancellationToken)
    {
        return await supplyRepository.GetSupplyByIdAsync(query.SupplyId, cancellationToken);
    }

    public async Task<int> Handle(GetTotalSupplyStockQuery query, CancellationToken cancellationToken)
    {
        return await supplyRepository.GetTotalCurrentStockAsync(cancellationToken);
    }
}
