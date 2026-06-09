using Aurora.SupplyWok.Platform.Iot.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Iot.Domain.Model.Aggregate;

/// <summary>
/// Represents the abstract base class for Alerts in the IoT bounded context.
/// </summary>
public abstract partial class Alert
{
    protected Alert(EAlertSeverity severity, string detail, DateTimeOffset date, EAlertStatus status) : this()
    {
        Severity = severity;
        Detail = detail;
        Date = date;
        Status = status;
    }

    public int Id { get; }
    
    public EAlertSeverity Severity { get; protected set; }
    
    public string Detail { get; protected set; }
    
    public DateTimeOffset Date { get; protected set; }
    
    public EAlertStatus Status { get; protected set; }

    /// <summary>
    /// Acknowledges the alert.
    /// </summary>
    public void Acknowledge()
    {
        Status = EAlertStatus.Acknowledged;
    }
}
