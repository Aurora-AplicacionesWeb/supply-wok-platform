using Aurora.SupplyWok.Platform.Iot.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Iot.Domain.Model.Aggregate;

public abstract partial class Alert
{
    protected Alert()
    {
        Detail = string.Empty;
        Severity = EAlertSeverity.Low;
        Date = DateTimeOffset.UtcNow;
        Status = EAlertStatus.Pending;
    }
}
