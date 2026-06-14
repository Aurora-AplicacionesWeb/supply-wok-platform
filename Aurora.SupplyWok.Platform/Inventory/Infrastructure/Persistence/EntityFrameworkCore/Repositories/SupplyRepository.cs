using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Entities;
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

    public async Task AddStockMovementAsync(StockMovement movement, CancellationToken cancellationToken)
    {
        await Context.Set<StockMovement>().AddAsync(movement, cancellationToken);
    }

    public async Task<IEnumerable<StockMovement>> ListStockMovementsAsync(CancellationToken cancellationToken)
    {
        return await Context.Set<StockMovement>()
            .Include(movement => movement.Supply)
            .ToListAsync(cancellationToken);
    }

    public async Task<StockMovement?> GetStockMovementByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await Context.Set<StockMovement>()
            .Include(movement => movement.Supply)
            .FirstOrDefaultAsync(movement => movement.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<StockMovement>> ListStockMovementsBySupplyIdAsync(int supplyId, CancellationToken cancellationToken)
    {
        return await Context.Set<StockMovement>()
            .Include(movement => movement.Supply)
            .Where(movement => movement.SupplyId == supplyId)
            .ToListAsync(cancellationToken);
    }
}
