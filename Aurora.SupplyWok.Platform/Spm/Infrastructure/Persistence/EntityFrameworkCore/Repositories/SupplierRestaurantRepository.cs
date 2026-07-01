using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Spm.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Spm.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class SupplierRestaurantRepository(AppDbContext context)
    : BaseRepository<SupplierRestaurant>(context), ISupplierRestaurantRepository
{
    public async Task<IEnumerable<SupplierRestaurant>> ListBySupplierProfileIdAsync(int supplierProfileId,
        CancellationToken cancellationToken)
    {
        return await Context.Set<SupplierRestaurant>()
            .Where(link => link.SupplierProfileId == supplierProfileId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<SupplierRestaurant>> ListByRestaurantProfileIdAsync(int restaurantProfileId,
        CancellationToken cancellationToken)
    {
        return await Context.Set<SupplierRestaurant>()
            .Where(link => link.RestaurantProfileId == restaurantProfileId)
            .ToListAsync(cancellationToken);
    }
}
