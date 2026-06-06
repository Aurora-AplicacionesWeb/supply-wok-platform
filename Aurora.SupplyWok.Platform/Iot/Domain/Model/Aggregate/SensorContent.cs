using Aurora.SupplyWok.Platform.Iot.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Iot.Domain.Model.Aggregate;

/// <summary>
/// Partial class for the <see cref="Sensor"/> aggregate
/// </summary>
public partial class Sensor {
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Sensor"/> class with default values.
    /// </summary>
    public Sensor()
    {
        Name = string.Empty;
        MinValue = 0;
        MaxValue = 0;
        Enabled = false;
        LastValue = 0;
        SensorType = ESensorType.Temperature;
    }
}