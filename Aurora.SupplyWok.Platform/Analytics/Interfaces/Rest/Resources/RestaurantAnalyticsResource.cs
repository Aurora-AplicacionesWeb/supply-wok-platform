using Aurora.SupplyWok.Platform.Analytics.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Analytics.Interfaces.Rest.Resources;

public record RestaurantAnalyticsResource(
    TrendData WeeklyConsumption
);
