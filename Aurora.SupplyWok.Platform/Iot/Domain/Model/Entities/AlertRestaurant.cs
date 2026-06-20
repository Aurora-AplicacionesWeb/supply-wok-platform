using Aurora.SupplyWok.Platform.Iot.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Iot.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Iot.Domain.Model.Entities;

public class AlertRestaurant : Alert
{
    protected AlertRestaurant() : base()
    {
    }

    public AlertRestaurant(EAlertSeverity severity, string detail, DateTimeOffset date, EAlertStatus status, int sensorId)
        : base(severity, detail, date, status)
    {
        SensorId = sensorId;
    }

    public int SensorId { get; private set; }
    
    public Sensor Sensor { get; internal set; } = null!;
}
