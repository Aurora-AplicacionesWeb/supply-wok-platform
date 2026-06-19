namespace Aurora.SupplyWok.Platform.Profiles.Domain.Model;

public enum ProfilesError
{
    None,
    RestaurantProfileNotFound,
    SupplierProfileNotFound,
    InvalidData,
    DuplicateEmail,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}