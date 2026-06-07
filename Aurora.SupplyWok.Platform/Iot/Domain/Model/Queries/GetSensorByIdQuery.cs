namespace Aurora.SupplyWok.Platform.Iot.Domain.Model.Queries;

/// <summary>
/// Represents a query to get a sensor by id in the Supply Wok Platform.
/// </summary>
/// <param name="SensorId">The id of the sensor to get</param>
public record GetSensorByIdQuery(int SensorId);