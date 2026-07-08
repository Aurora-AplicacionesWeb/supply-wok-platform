using Aurora.SupplyWok.Platform.Analytics.Application.QueryServices;
using Aurora.SupplyWok.Platform.Analytics.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Analytics.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Analytics.Domain.Model.ValueObjects;
using Aurora.SupplyWok.Platform.Operations.Interfaces.Acl;

namespace Aurora.SupplyWok.Platform.Analytics.Application.Internal.QueryServices;

public class RestaurantAnalyticsQueryService(IOperationsContextFacade operationsFacade) : IRestaurantAnalyticsQueryService
{
    public async Task<IEnumerable<RestaurantAnalytics>> Handle(GetAllRestaurantAnalyticsQuery query, CancellationToken cancellationToken)
    {
        var dto = await operationsFacade.GetWeeklyConsumptionAsync(cancellationToken: cancellationToken);

        var weeklyConsumption = new TrendData(dto.Labels, dto.Data);

        var analytics = new RestaurantAnalytics(weeklyConsumption);

        return new[] { analytics };
    }
}
