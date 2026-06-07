using Aurora.SupplyWok.Platform.Iot.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Iot.Interfaces.Rest.Resources;

/// <summary>
/// Resource for create a new sensor
/// </summary>
/// <param name="Name">The name of the sensor</param>
/// <param name="MinValue">The minimum value of the sensor</param>
/// <param name="MaxValue">The maximum value of the sensor</param>
/// <param name="Enabled">Refers if the sensor is enabled or not</param>
/// <param name="LastValue">The last value of the sensor</param>
/// <param name="Type">The type of the sensor</param>
public record CreateSensorResource(
    string Name,
    double MinValue,
    double MaxValue,
    bool Enabled,
    double LastValue,
    ESensorType Type);