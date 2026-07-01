namespace Aurora.SupplyWok.Platform.Spm.Interfaces.Rest.Resources;

public record UpdateCatalogItemResource(
    string Name,
    string Category,
    decimal Price,
    string Unit,
    string DeliveryConditions);
