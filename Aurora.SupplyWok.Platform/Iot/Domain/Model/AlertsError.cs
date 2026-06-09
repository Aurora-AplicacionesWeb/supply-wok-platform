namespace Aurora.SupplyWok.Platform.Iot.Domain.Model;

public enum AlertsError
{
    None,
    AlertNotFound,
    SensorNotFound,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
