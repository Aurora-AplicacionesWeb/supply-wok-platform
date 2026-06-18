namespace Aurora.SupplyWok.Platform.Suppliers.Domain.Model;

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
