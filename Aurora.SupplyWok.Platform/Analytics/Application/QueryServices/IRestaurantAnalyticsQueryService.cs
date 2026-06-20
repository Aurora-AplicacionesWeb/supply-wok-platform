using Aurora.SupplyWok.Platform.Analytics.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Analytics.Domain.Model.Queries;

namespace Aurora.SupplyWok.Platform.Analytics.Application.QueryServices;

public interface IRestaurantAnalyticsQueryService
{
    Task<IEnumerable<RestaurantAnalytics>> Handle(GetAllRestaurantAnalyticsQuery query, CancellationToken cancellationToken);
}
