namespace Aurora.SupplyWok.Platform.Operations.Domain.Model;

public enum OperationsError
{
    None,
    TableNotFound,
    DishNotFound,
    KitchenOrderNotFound,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}