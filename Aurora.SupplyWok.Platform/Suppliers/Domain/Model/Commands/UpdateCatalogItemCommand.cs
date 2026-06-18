using Aurora.SupplyWok.Platform.Suppliers.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Commands;

public record UpdateCatalogItemCommand(
    int SupplierId,
    int CatalogItemId,
    string Name,
    string Category,
    decimal Price,
    ECatalogUnit Unit,
    string DeliveryConditions);
