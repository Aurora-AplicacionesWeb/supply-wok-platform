using Aurora.SupplyWok.Platform.Iot.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Iot.Domain.Model.Aggregate;

public partial class Sensor {
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