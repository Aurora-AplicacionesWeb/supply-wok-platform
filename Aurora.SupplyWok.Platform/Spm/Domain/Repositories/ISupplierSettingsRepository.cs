using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;

namespace Aurora.SupplyWok.Platform.Spm.Domain.Repositories;

public interface ISupplierSettingsRepository : IBaseRepository<SupplierSettings>
{
    Task<SupplierSettings?> FindBySupplierProfileIdAsync(int supplierProfileId, CancellationToken cancellationToken);
}
