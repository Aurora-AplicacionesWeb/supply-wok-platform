using System.ComponentModel.DataAnnotations;

namespace Aurora.SupplyWok.Platform.Iot.Interfaces.Rest.Resources;

/// <summary>
/// Resource for creating a restaurant alert
/// </summary>
public record CreateAlertRestaurantResource(
    [Required] string Severity,
    [Required] string Detail,
    [Required] int SensorId
);
