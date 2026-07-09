using Aurora.SupplyWok.Platform.Analytics.Application.QueryServices;
using Aurora.SupplyWok.Platform.Analytics.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Analytics.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Analytics.Domain.Model.ValueObjects;
using Aurora.SupplyWok.Platform.Operations.Interfaces.Acl;

namespace Aurora.SupplyWok.Platform.Analytics.Application.Internal.QueryServices;

public class RestaurantAnalyticsQueryService(
    IOperationsContextFacade operationsFacade) : IRestaurantAnalyticsQueryService
{
    public async Task<IEnumerable<RestaurantAnalytics>> Handle(
        GetAllRestaurantAnalyticsQuery query,
        CancellationToken cancellationToken)
    {
        var dto = await operationsFacade.GetWeeklyConsumptionAsync(cancellationToken);

        var analytics = new RestaurantAnalytics(new TrendData(dto.Labels, dto.Data));

        return new[] { analytics };
    }
}
