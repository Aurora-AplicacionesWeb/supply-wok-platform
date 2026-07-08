using Aurora.SupplyWok.Platform.Analytics.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Analytics.Domain.Model.Aggregates;

public partial class RestaurantAnalytics
{
    public RestaurantAnalytics(TrendData weeklyConsumption) : this()
    {
        WeeklyConsumption = weeklyConsumption;
    }

    public int Id { get; private set; }
    public TrendData WeeklyConsumption { get; private set; }
}
