namespace Aurora.SupplyWok.Platform.Iot.Interfaces.Rest.Resources;

/// <summary>
/// Base Alert resource for REST API
/// </summary>
public record AlertResource(
    int Id,
    string Severity,
    string Detail,
    DateTimeOffset Date,
    string Status,
    string AlertType
);
