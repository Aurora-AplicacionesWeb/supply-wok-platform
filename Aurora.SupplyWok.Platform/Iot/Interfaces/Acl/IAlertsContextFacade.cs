using Aurora.SupplyWok.Platform.Iot.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Iot.Interfaces.Acl;

/// <summary>
/// Facade for the Alert context
/// </summary>
public interface IAlertsContextFacade
{
    /// <summary>
    /// Creates a new restaurant alert with the specified parameters.
    /// </summary>
    /// <param name="severity">The severity of the alert.</param>
    /// <param name="detail">The detail text of the alert.</param>
    /// <param name="sensorId">The identifier of the sensor triggering the alert.</param>
    /// <param name="cancellationToken">A cancellation token to signal the request should be canceled.</param>
    /// <returns>A task representing the asynchronous operation with an integer that uniquely identifies the created alert.</returns>
    Task<int> CreateAlertRestaurant(string severity, string detail, int sensorId, CancellationToken cancellationToken);
}
