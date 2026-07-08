using Aurora.SupplyWok.Platform.Spm.Application.QueryServices;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Spm.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Spm.Application.Internal.QueryServices;

public class SupplierSettingsQueryService(ISupplierSettingsRepository supplierSettingsRepository) : ISupplierSettingsQueryService
{
    public async Task<SupplierSettings?> Handle(GetSupplierSettingsBySupplierProfileIdQuery query, CancellationToken cancellationToken)
    {
        return await supplierSettingsRepository.FindBySupplierProfileIdAsync(query.SupplierProfileId, cancellationToken);
    }
}
