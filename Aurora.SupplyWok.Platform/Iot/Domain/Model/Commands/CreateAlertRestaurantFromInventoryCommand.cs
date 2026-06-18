namespace Aurora.SupplyWok.Platform.Iot.Domain.Model.Commands;

/// <summary>
/// Command to create a restaurant alert when inventory stock differs from the last sensor value.
/// </summary>
/// <param name="SensorId">The identifier of the sensor that tracks inventory stock.</param>
public record CreateAlertRestaurantFromInventoryCommand(int SensorId);
