namespace Aurora.SupplyWok.Platform.Suppliers.Interfaces.Rest.Resources;

public record CreateCatalogItemResource(
    string Name,
    string Category,
    decimal Price,
    string Unit,
    string DeliveryConditions);
