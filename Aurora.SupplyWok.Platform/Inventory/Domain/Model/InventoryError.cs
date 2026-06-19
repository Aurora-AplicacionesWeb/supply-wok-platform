namespace Aurora.SupplyWok.Platform.Inventory.Domain.Model;

public enum InventoryError
{
    None,
    SupplyNotFound,
    InventoryTransactionNotFound,
    InvalidData,
    InsufficientStock,
    TransferNotSupported,
    OperationCancelled,
    DatabaseError,
    InternalServerError   
}
