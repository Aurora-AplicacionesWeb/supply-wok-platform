namespace Aurora.SupplyWok.Platform.Suppliers.Interfaces.Rest.Resources;

/// <summary>
///     REST resource returned when listing supplier clients.
/// </summary>
public record ClientResource(
    int Id,
    string Name,
    string District,
    string Status);
