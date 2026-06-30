namespace Aurora.SupplyWok.Platform.Iam.Domain.Model;

public enum IamError
{
    None,
    UserNotFound,
    EmailAlreadyTaken,
    InvalidCredentials,
    OperationCancelled,
    DatabaseError,
    InternalServerError,
    ExternalServiceError
}