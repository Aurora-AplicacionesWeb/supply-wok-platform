namespace Aurora.SupplyWok.Platform.Operations.Domain.Model;

public enum OperationsError
{
    None,
    TableNotFound,
    DishNotFound,
    DishCategoryNotFound,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}