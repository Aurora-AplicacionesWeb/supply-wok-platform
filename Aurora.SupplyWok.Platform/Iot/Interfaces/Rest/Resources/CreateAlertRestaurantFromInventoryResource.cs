using System.ComponentModel.DataAnnotations;

namespace Aurora.SupplyWok.Platform.Iot.Interfaces.Rest.Resources;

/// <summary>
/// Resource for creating a restaurant alert from the current inventory state.
/// </summary>
/// <param name="SensorId">The identifier of the sensor that tracks inventory stock.</param>
public record CreateAlertRestaurantFromInventoryResource([Required] int SensorId);
