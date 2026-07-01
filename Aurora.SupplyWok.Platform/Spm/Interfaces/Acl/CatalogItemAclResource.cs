namespace Aurora.SupplyWok.Platform.Spm.Interfaces.Acl;

public record CatalogItemAclResource(
    int Id,
    int SupplierId,
    string Name,
    string Category,
    decimal Price,
    string Unit,
    string DeliveryConditions);
