namespace Aurora.SupplyWok.Platform.Iot.Domain.Model.Commands;

/// <summary>
/// Command to delete a sensor
/// </summary>
/// <param name="Id">The unique identifier of the sensor to delete</param>
public record DeleteSensorCommand(int Id);
