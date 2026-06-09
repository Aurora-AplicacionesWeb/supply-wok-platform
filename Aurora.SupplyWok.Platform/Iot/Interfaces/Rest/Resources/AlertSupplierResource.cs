namespace Aurora.SupplyWok.Platform.Iot.Interfaces.Rest.Resources;

/// <summary>
/// Supplier alert resource.
/// </summary>
public record AlertSupplierResource(
    int Id,
    string Severity,
    string Detail,
    DateTimeOffset Date,
    string Status,
    string AlertType
) : AlertResource(Id, Severity, Detail, Date, Status, AlertType);
