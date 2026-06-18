using Aurora.SupplyWok.Platform.Operations.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Operations.Domain.Repositories;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Operations.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class KitchenOrderRepository(AppDbContext context)
    : BaseRepository<KitchenOrder>(context), IKitchenOrderRepository
{
    public async Task<KitchenOrder?> FindByIdWithItemsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<KitchenOrder>()
            .Include(ko => ko.Items)
            .FirstOrDefaultAsync(ko => ko.Id == id, cancellationToken);
    }
}
