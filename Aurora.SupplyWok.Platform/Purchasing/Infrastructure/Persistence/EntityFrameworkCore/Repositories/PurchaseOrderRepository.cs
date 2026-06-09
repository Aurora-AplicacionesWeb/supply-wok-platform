using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Purchasing.Domain.Repositories;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Purchasing.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class PurchaseOrderRepository(AppDbContext context) : BaseRepository<PurchaseOrder>(context), IPurchaseOrderRepository
{
    public async Task<IEnumerable<PurchaseOrder>> ListPurchaseOrdersAsync(CancellationToken cancellationToken)
    {
        return await Context.Set<PurchaseOrder>()
            .Include(order => order.Items)
            .ToListAsync(cancellationToken);
    }

    public async Task<PurchaseOrder?> GetPurchaseOrderByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await Context.Set<PurchaseOrder>()
            .Include(order => order.Items)
            .FirstOrDefaultAsync(order => order.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsByCodeAsync(string code, int? excludedId, CancellationToken cancellationToken)
    {
        return await Context.Set<PurchaseOrder>()
            .AnyAsync(order => order.Code == code && (!excludedId.HasValue || order.Id != excludedId.Value), cancellationToken);
    }
}
