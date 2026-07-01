using Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Queries;

namespace Aurora.SupplyWok.Platform.Spm.Application.QueryServices;

public interface ISupplierRestaurantQueryService
{
    Task<IEnumerable<SupplierRestaurant>> Handle(GetSuppliersByRestaurantIdQuery query,
        CancellationToken cancellationToken);

    Task<IEnumerable<SupplierRestaurant>> Handle(GetRestaurantsBySupplierIdQuery query,
        CancellationToken cancellationToken);
}
