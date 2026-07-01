using Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Queries;

namespace Aurora.SupplyWok.Platform.Spm.Application.QueryServices;

public interface ICatalogItemQueryService
{
    Task<IEnumerable<CatalogItem>> Handle(GetAllCatalogItemsBySupplierIdQuery query, CancellationToken cancellationToken);
    Task<CatalogItem?> Handle(GetCatalogItemByIdQuery query, CancellationToken cancellationToken);
}
