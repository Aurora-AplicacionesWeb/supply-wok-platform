using Aurora.SupplyWok.Platform.Iot.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Iot.Domain.Model.Commands;

/// <summary>
/// Command to update an existing sensor
/// </summary>
/// <param name="Id">The unique identifier of the sensor to update</param>
/// <param name="Name">The updated name of the sensor</param>
/// <param name="MinValue">The updated minimum value</param>
/// <param name="MaxValue">The updated maximum value</param>
/// <param name="Enabled">The updated enabled status</param>
/// <param name="LastValue">The updated last value</param>
/// <param name="Type">The updated type of the sensor</param>
public record UpdateSensorCommand(int Id, string Name, double MinValue, double MaxValue, bool Enabled, double LastValue, ESensorType Type);
