namespace Aurora.SupplyWok.Platform.Suppliers.Interfaces.Rest.Resources;

public record UpdateCatalogItemResource(
    string Name,
    string Category,
    decimal Price,
    string Unit,
    string DeliveryConditions);
