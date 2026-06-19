using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Inventory.Domain.Repositories;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Inventory.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class SupplyRepository(AppDbContext context) : BaseRepository<Supply>(context), ISupplyRepository
{
    public async Task<Supply?> GetSupplyByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await Context.Set<Supply>().FirstOrDefaultAsync(supply => supply.Id == id, cancellationToken);
    }

    public async Task<int> GetTotalCurrentStockAsync(CancellationToken cancellationToken)
    {
        return await Context.Set<Supply>()
            .Select(supply => (int?)supply.CurrentStock)
            .SumAsync(cancellationToken) ?? 0;
    }
}
