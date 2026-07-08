using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Spm.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Spm.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class SupplierSettingsRepository(AppDbContext context) : BaseRepository<SupplierSettings>(context), ISupplierSettingsRepository
{
    public async Task<SupplierSettings?> FindBySupplierProfileIdAsync(int supplierProfileId, CancellationToken cancellationToken)
    {
        return await Context.Set<SupplierSettings>()
            .FirstOrDefaultAsync(settings => settings.SupplierProfileId == supplierProfileId, cancellationToken);
    }
}
