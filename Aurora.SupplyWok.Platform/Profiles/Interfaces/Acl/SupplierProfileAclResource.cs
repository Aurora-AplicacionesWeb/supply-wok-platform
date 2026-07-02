namespace Aurora.SupplyWok.Platform.Profiles.Interfaces.Acl;

public record SupplierProfileAclResource(
    int Id,
    string BusinessName,
    string ContactName,
    string Email,
    string Phone,
    string Category,
    string Status);
