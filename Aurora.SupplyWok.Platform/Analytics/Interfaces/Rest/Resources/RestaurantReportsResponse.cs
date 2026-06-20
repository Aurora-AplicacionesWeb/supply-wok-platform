using System.Text.Json.Serialization;

namespace Aurora.SupplyWok.Platform.Analytics.Interfaces.Rest.Resources;

public record RestaurantReportsResponse(
    [property: JsonPropertyName("restaurant-reports")] RestaurantAnalyticsResource RestaurantReports
);
