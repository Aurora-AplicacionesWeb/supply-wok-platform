using Aurora.SupplyWok.Platform.Spm.Application.QueryServices;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Spm.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Spm.Application.Internal.QueryServices;

public class SupplierQueryService(ISupplierRepository supplierRepository) : ISupplierQueryService
{
    public async Task<IEnumerable<SupplierReference>> Handle(GetAllSuppliersQuery query, CancellationToken cancellationToken)
    {
        return await supplierRepository.ListAsync(cancellationToken);
    }
}
