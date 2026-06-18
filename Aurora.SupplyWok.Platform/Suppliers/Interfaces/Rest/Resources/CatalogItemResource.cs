namespace Aurora.SupplyWok.Platform.Suppliers.Interfaces.Rest.Resources;

public record CatalogItemResource(
    int Id,
    string Name,
    string Category,
    decimal Price,
    string Unit,
    string DeliveryConditions);
