using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Inventory.Domain.Repositories;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Inventory.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class InventoryTransactionRepository(AppDbContext context)
    : BaseRepository<InventoryTransaction>(context), IInventoryTransactionRepository
{
    public async Task<IEnumerable<InventoryTransaction>> FindBySupplyIdAsync(int supplyId, CancellationToken cancellationToken)
    {
        return await Context.Set<InventoryTransaction>()
            .Include(t => t.Operations)
            .Where(t => t.SupplyId == supplyId)
            .ToListAsync(cancellationToken);
    }

    public async Task<InventoryTransaction?> FindWithOperationsByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await Context.Set<InventoryTransaction>()
            .Include(t => t.Operations)
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }
}
