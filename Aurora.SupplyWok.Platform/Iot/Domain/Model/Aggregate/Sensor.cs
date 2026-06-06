using Aurora.SupplyWok.Platform.Iot.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Iot.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Iot.Domain.Model.Aggregate;

public partial class Sensor
{
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
}