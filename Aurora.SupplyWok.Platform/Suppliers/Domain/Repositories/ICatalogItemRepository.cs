using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Aggregates;

namespace Aurora.SupplyWok.Platform.Suppliers.Domain.Repositories;

public interface ICatalogItemRepository : IBaseRepository<CatalogItem>
{
    Task<IEnumerable<CatalogItem>> ListBySupplierIdAsync(int supplierId, CancellationToken cancellationToken);
    Task<CatalogItem?> FindByIdAndSupplierIdAsync(int catalogItemId, int supplierId, CancellationToken cancellationToken);
}
