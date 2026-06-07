using Aurora.SupplyWok.Platform.Iot.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Iot.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Iot.Domain.Model.Aggregate;

/// <summary>
/// Represents the Sensor aggregate in the Supply Wok Platform
/// </summary>
public partial class Sensor
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Sensor"/> aggregate.
    /// </summary>
    /// <param name="name">The name of the sensor</param>
    /// <param name="minValue">The minimum value of the sensor</param>
    /// <param name="maxValue">The maximum value of the sensor</param>
    /// <param name="enabled">Refers if the sensor is enabled or not</param>
    /// <param name="lastValue">The last value of the sensor</param>
    /// <param name="type">The type of the sensor</param>
    public Sensor(string name, double minValue, double maxValue, bool enabled, double lastValue, ESensorType type) : this()
    {
        Name = name;
        MinValue = minValue;
        MaxValue = maxValue;
        Enabled = enabled;
        LastValue = lastValue;
        SensorType = type;
    }
    
    public Sensor(CreateSensorCommand command): this(command.Name, command.MinValue, command.MaxValue, command.Enabled, command.LastValue, command.Type)
    {
    }
    
    public int Id { get; }
    
    public string Name { get; private set; }
    
    public double MinValue { get; private set;}
    
    public double MaxValue { get; private set;}
    
    public bool Enabled { get; private set;}
    
    public double LastValue { get; private set;}
    
    public ESensorType SensorType { get; private set;}

    public void Update(string name, double minValue, double maxValue, bool enabled, double lastValue, ESensorType type)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Sensor name cannot be empty.", nameof(name));
        if (minValue > maxValue)
            throw new ArgumentException("Minimum value cannot be greater than maximum value.");

        Name = name;
        MinValue = minValue;
        MaxValue = maxValue;
        Enabled = enabled;
        LastValue = lastValue;
        SensorType = type;
    }
}