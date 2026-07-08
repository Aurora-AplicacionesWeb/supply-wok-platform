using Aurora.SupplyWok.Platform.Analytics.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Analytics.Domain.Model.Aggregates;

public partial class RestaurantAnalytics
{
    public RestaurantAnalytics()
    {
        WeeklyConsumption = new TrendData();
    }
}
