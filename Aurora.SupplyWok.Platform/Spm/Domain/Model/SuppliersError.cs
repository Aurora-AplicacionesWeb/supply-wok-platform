namespace Aurora.SupplyWok.Platform.Spm.Domain.Model;

public enum SuppliersError
{
    None,
    SupplierNotFound,
    CatalogItemNotFound,
    InvalidData,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
