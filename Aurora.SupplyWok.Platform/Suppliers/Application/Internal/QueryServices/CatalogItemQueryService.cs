using Aurora.SupplyWok.Platform.Suppliers.Application.QueryServices;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Suppliers.Application.Internal.QueryServices;

public class CatalogItemQueryService(ICatalogItemRepository catalogItemRepository) : ICatalogItemQueryService
{
    public async Task<IEnumerable<CatalogItem>> Handle(GetAllCatalogItemsBySupplierIdQuery query, CancellationToken cancellationToken)
    {
        return await catalogItemRepository.ListBySupplierIdAsync(query.SupplierId, cancellationToken);
    }

    public async Task<CatalogItem?> Handle(GetCatalogItemByIdQuery query, CancellationToken cancellationToken)
    {
        return await catalogItemRepository.FindByIdAndSupplierIdAsync(query.CatalogItemId, query.SupplierId, cancellationToken);
    }
}
