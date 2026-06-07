using Aurora.SupplyWok.Platform.Iot.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Iot.Interfaces.Acl;

/// <summary>
/// Facade for the Sensor context
/// </summary>
public interface ISensorContextFacade
{
    /// <summary>
    /// Creates a new sensor with the specified parameters.
    /// </summary>
    /// <param name="name">The name of the sensor.</param>
    /// <param name="minValue">The minimum allowable value for the sensor readings.</param>
    /// <param name="maxValue">The maximum allowable value for the sensor readings.</param>
    /// <param name="enabled">A value indicating whether the sensor is enabled.</param>
    /// <param name="lastValue">The last recorded value of the sensor.</param>
    /// <param name="sensorType">The type of the sensor represented by the <see cref="ESensorType"/> enumeration.</param>
    /// <param name="cancellationToken">A cancellation token to signal the request should be canceled.</param>
    /// <returns>A task representing the asynchronous operation with an integer that uniquely identifies the created sensor.</returns>
    Task<int> CreateSensor(string name,
        double minValue,
        double maxValue,
        bool enabled,
        double lastValue,
        ESensorType sensorType,
        CancellationToken cancellationToken);
}