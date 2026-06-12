using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Aggregates;

namespace Aurora.SupplyWok.Platform.Suppliers.Domain.Repositories;

public interface ISupplierRepository : IBaseRepository<Supplier>
{
    Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken);
}
