using Aurora.SupplyWok.Platform.Iot.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Iot.Domain.Model.Commands;

/// <summary>
/// Command to create a new restaurant alert
/// </summary>
/// <param name="Severity">The severity of the alert</param>
/// <param name="Detail">The detail text of the alert</param>
/// <param name="SensorId">The identifier of the sensor triggering the alert</param>
public record CreateAlertRestaurantCommand(EAlertSeverity Severity, string Detail, int SensorId);
