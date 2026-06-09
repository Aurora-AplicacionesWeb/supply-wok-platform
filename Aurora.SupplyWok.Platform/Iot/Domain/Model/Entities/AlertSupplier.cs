using Aurora.SupplyWok.Platform.Iot.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Iot.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Iot.Domain.Model.Entities;

public class AlertSupplier : Alert
{
    protected AlertSupplier() : base()
    {
    }

    public AlertSupplier(EAlertSeverity severity, string detail, DateTimeOffset date, EAlertStatus status)
        : base(severity, detail, date, status)
    {
    }
}
