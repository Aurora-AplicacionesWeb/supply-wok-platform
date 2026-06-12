using Aurora.SupplyWok.Platform.Suppliers.Application.QueryServices;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Suppliers.Application.Internal.QueryServices;

public class SupplierQueryService(ISupplierRepository supplierRepository) : ISupplierQueryService
{
    public async Task<IEnumerable<Supplier>> Handle(GetAllSuppliersQuery query, CancellationToken cancellationToken)
    {
        return await supplierRepository.ListAsync(cancellationToken);
    }
}
