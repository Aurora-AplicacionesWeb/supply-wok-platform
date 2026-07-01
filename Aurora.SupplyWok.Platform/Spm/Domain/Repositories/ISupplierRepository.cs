using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;

namespace Aurora.SupplyWok.Platform.Spm.Domain.Repositories;

public interface ISupplierRepository : IBaseRepository<SupplierReference>
{
    Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken);
}
