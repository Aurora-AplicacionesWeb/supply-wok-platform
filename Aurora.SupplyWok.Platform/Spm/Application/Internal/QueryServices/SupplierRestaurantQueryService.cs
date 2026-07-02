using Aurora.SupplyWok.Platform.Spm.Application.QueryServices;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Spm.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Spm.Application.Internal.QueryServices;

public class SupplierRestaurantQueryService(ISupplierRestaurantRepository supplierRestaurantRepository)
    : ISupplierRestaurantQueryService
{
    public async Task<IEnumerable<SupplierRestaurant>> Handle(GetSuppliersByRestaurantIdQuery query,
        CancellationToken cancellationToken)
    {
        return await supplierRestaurantRepository.ListByRestaurantProfileIdAsync(query.RestaurantId,
            cancellationToken);
    }

    public async Task<IEnumerable<SupplierRestaurant>> Handle(GetRestaurantsBySupplierIdQuery query,
        CancellationToken cancellationToken)
    {
        return await supplierRestaurantRepository.ListBySupplierProfileIdAsync(query.SupplierId,
            cancellationToken);
    }
}
