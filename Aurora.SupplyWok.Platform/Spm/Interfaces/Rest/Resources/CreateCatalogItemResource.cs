namespace Aurora.SupplyWok.Platform.Spm.Interfaces.Rest.Resources;

public record CreateCatalogItemResource(
    string Name,
    string Category,
    decimal Price,
    string Unit,
    string DeliveryConditions);
