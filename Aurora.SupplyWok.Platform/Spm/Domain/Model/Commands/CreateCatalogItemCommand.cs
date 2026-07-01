using Aurora.SupplyWok.Platform.Spm.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Spm.Domain.Model.Commands;

public record CreateCatalogItemCommand(
    int SupplierId,
    string Name,
    string Category,
    decimal Price,
    ECatalogUnit Unit,
    string DeliveryConditions);
