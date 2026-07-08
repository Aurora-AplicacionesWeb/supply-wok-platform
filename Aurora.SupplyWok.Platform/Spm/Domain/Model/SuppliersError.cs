namespace Aurora.SupplyWok.Platform.Spm.Domain.Model;

public enum SuppliersError
{
    None,
    SupplierNotFound,
    CatalogItemNotFound,
    SupplierSettingsNotFound,
    InvalidData,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
