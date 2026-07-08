using Aurora.SupplyWok.Platform.Analytics.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Analytics.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Analytics.Interfaces.Rest.Transform;

public static class RestaurantAnalyticsResourceFromEntityAssembler
{
    public static RestaurantAnalyticsResource ToResourceFromEntity(RestaurantAnalytics entity)
    {
        return new RestaurantAnalyticsResource(entity.WeeklyConsumption);
    }
}
