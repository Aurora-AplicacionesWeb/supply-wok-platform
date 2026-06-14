namespace Aurora.SupplyWok.Platform.Inventory.Domain.Model;

public enum InventoryError
{
    None,
    SupplyNotFound,
    StockMovementNotFound,
    InvalidData,
    InsufficientStock,
    OperationCancelled,
    DatabaseError,
    InternalServerError   
}
