namespace Aurora.SupplyWok.Platform.Iot.Interfaces.Rest.Resources;

/// <summary>
/// Restaurant alert resource containing details about the sensor that triggered it.
/// </summary>
public record AlertRestaurantResource(
    int Id,
    string Severity,
    string Detail,
    DateTimeOffset Date,
    string Status,
    string AlertType,
    int SensorId,
    string SensorName
) : AlertResource(Id, Severity, Detail, Date, Status, AlertType);
