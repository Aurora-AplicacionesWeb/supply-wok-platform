namespace Aurora.SupplyWok.Platform.Purchasing.Interfaces.Rest.Resources;

public record SupplierResource(
    int Id,
    Guid Uuid,
    string Name,
    string ContactName,
    string Email,
    string Phone,
    string Category,
    string LinkedDate,
    string Sla,
    string ResponseTime);
