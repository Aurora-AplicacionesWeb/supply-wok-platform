using Aurora.SupplyWok.Platform.Analytics.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Analytics.Domain.Model.Aggregates;

public partial class RestaurantAnalytics
{
    public RestaurantAnalytics(
        TrendData inventoryTrend,
        TrendData weeklyConsumption,
        TrendData temperatureFluctuations,
        ICollection<TopSupplierOrder> topSuppliersOrders) : this()
    {
        InventoryTrend = inventoryTrend;
        WeeklyConsumption = weeklyConsumption;
        TemperatureFluctuations = temperatureFluctuations;
        TopSuppliersOrders = topSuppliersOrders;
    }

    public int Id { get; private set; }
    public TrendData InventoryTrend { get; private set; }
    public TrendData WeeklyConsumption { get; private set; }
    public TrendData TemperatureFluctuations { get; private set; }
    public ICollection<TopSupplierOrder> TopSuppliersOrders { get; private set; }
}
