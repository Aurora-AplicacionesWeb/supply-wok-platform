using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;

namespace Aurora.SupplyWok.Platform.Spm.Domain.Repositories;

public interface ISupplierRestaurantRepository : IBaseRepository<SupplierRestaurant>
{
    Task<IEnumerable<SupplierRestaurant>> ListBySupplierProfileIdAsync(int supplierProfileId,
        CancellationToken cancellationToken);

    Task<IEnumerable<SupplierRestaurant>> ListByRestaurantProfileIdAsync(int restaurantProfileId,
        CancellationToken cancellationToken);
}
