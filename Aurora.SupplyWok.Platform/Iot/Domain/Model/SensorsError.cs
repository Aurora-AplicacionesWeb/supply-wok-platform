namespace Aurora.SupplyWok.Platform.Iot.Domain.Model;

public enum SensorsError
{
    None,
    SensorNotFound,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}