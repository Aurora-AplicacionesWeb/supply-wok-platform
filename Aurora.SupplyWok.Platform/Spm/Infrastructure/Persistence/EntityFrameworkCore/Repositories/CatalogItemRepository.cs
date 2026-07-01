using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Spm.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Spm.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class CatalogItemRepository(AppDbContext context) : BaseRepository<CatalogItem>(context), ICatalogItemRepository
{
    public async Task<IEnumerable<CatalogItem>> ListBySupplierIdAsync(int supplierId, CancellationToken cancellationToken)
    {
        return await Context.Set<CatalogItem>()
            .Where(catalogItem => catalogItem.SupplierId == supplierId)
            .ToListAsync(cancellationToken);
    }

    public async Task<CatalogItem?> FindByIdAndSupplierIdAsync(int catalogItemId, int supplierId, CancellationToken cancellationToken)
    {
        return await Context.Set<CatalogItem>()
            .FirstOrDefaultAsync(
                catalogItem => catalogItem.Id == catalogItemId && catalogItem.SupplierId == supplierId,
                cancellationToken);
    }
}
