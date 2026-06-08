using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Purchasing.Domain.Repositories;

public interface ISupplierRepository : IBaseRepository<Supplier>
{
    Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken);
}
