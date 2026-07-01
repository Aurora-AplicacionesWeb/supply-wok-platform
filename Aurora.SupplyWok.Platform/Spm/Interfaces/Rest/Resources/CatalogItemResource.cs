namespace Aurora.SupplyWok.Platform.Spm.Interfaces.Rest.Resources;

public record CatalogItemResource(
    int Id,
    string Name,
    string Category,
    decimal Price,
    string Unit,
    string DeliveryConditions);
