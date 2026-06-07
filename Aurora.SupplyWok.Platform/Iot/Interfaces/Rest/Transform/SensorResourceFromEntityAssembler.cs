using Aurora.SupplyWok.Platform.Iot.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Iot.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Iot.Interfaces.Rest.Transform;

public static class SensorResourceFromEntityAssembler
{
    /// <summary>
    /// Convert a <see cref="Sensor"/> to a <see cref="SensorResource"/>
    /// </summary>
    /// <param name="sensor">The <see cref="Sensor"/> aggregate to convert. Must not be null</param>
    /// <returns>A <see cref="SensorResource" /> object representing the provided sensor.</returns>
    /// <exception cref="ArgumentException">Thrown if the input <paramref name="sensor" /> is null</exception>
    public static SensorResource ToResourceFromEntity(Sensor sensor)
    {
        if (sensor == null)
            throw new ArgumentException(nameof(sensor),
                "Sensor entity cannot be null when converting to resource.");
        return new SensorResource(sensor.Id,
            sensor.Name,
            sensor.MinValue,
            sensor.MaxValue,
            sensor.Enabled,
            sensor.LastValue,
            sensor.SensorType);
    }
    
    
}