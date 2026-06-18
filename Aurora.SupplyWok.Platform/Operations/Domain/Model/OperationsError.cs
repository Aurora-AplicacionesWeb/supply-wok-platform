namespace Aurora.SupplyWok.Platform.Operations.Domain.Model;

public enum OperationsError
{
    None,
    TableNotFound,
    DishNotFound,
    DishCategoryNotFound,
    KitchenOrderNotFound,
    KitchenOrderItemNotFound,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}