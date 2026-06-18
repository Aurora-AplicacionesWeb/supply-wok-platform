using Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Queries;

namespace Aurora.SupplyWok.Platform.Suppliers.Application.QueryServices;

public interface ICatalogItemQueryService
{
    Task<IEnumerable<CatalogItem>> Handle(GetAllCatalogItemsBySupplierIdQuery query, CancellationToken cancellationToken);
    Task<CatalogItem?> Handle(GetCatalogItemByIdQuery query, CancellationToken cancellationToken);
}
