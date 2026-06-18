using Aurora.SupplyWok.Platform.Operations.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Operations.Domain.Repositories;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Operations.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class DishRepository(AppDbContext context) : BaseRepository<Dish>(context), IDishRepository
{
    public async Task<IEnumerable<Dish>> ListWithCategoryAsync(CancellationToken cancellationToken)
    {
        return await Context.Set<Dish>()
            .Include(d => d.DishCategory)
            .ToListAsync(cancellationToken);
    }
}
