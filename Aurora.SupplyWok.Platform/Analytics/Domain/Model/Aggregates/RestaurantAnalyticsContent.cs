using Aurora.SupplyWok.Platform.Analytics.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Analytics.Domain.Model.Aggregates;

public partial class RestaurantAnalytics
{
    public RestaurantAnalytics()
    {
        InventoryTrend = new TrendData();
        WeeklyConsumption = new TrendData();
        TemperatureFluctuations = new TrendData();
        TopSuppliersOrders = new List<TopSupplierOrder>();
    }
}
