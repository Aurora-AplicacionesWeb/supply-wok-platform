namespace Aurora.SupplyWok.Platform.Purchasing.Domain.Model;

public enum PurchaseOrdersError
{
    None,
    PurchaseOrderNotFound,
    SupplierNotFound,
    DuplicateCode,
    InvalidData,
    InvalidStatusTransition,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
