namespace Aurora.SupplyWok.Platform.Operations.Domain.Model;

public enum OperationsError
{
    None,
    TableNotFound,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}