using Aurora.SupplyWok.Platform.Iot.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Iot.Domain.Model.Commands;

/// <summary>
/// Command to create a new sensor
/// </summary>
/// <param name="name">The name of the sensor to create</param>
/// <param name="minValue">The minimum value of the sensor</param>
/// <param name="maxValue">The maximum value of the sensor</param>
/// <param name="enabled">Indicates if the sensor is enabled</param>
/// <param name="lastValue">The last value of the sensor</param>
/// <param name="type">The type of the sensor</param>
public record CreateSensorCommand(string Name, double MinValue, double MaxValue, bool Enabled, double LastValue, ESensorType Type);