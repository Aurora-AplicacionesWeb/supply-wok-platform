using Aurora.SupplyWok.Platform.Iot.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Iot.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Iot.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Iot.Interfaces.Rest.Transform;

/// <summary>
/// Polymorphic assembler to convert Alert entity to AlertResource (or derived resource types).
/// </summary>
public static class AlertResourceFromEntityAssembler
{
    /// <summary>
    /// Convert an Alert entity to an AlertResource.
    /// </summary>
    /// <param name="alert">The alert entity to convert</param>
    /// <returns>A concrete AlertResource (AlertRestaurantResource or AlertSupplierResource)</returns>
    /// <exception cref="ArgumentNullException">Thrown when the entity is null</exception>
    public static AlertResource ToResourceFromEntity(Alert alert)
    {
        if (alert == null)
            throw new ArgumentNullException(nameof(alert),
                "Alert entity cannot be null when converting to resource.");

        var severity = alert.Severity.ToString().ToLower();
        var status = alert.Status.ToString().ToLower();

        if (alert is AlertRestaurant alertRestaurant)
        {
            var sensorResource = alertRestaurant.Sensor != null 
                ? SensorResourceFromEntityAssembler.ToResourceFromEntity(alertRestaurant.Sensor)
                : new SensorResource(
                    alertRestaurant.SensorId, 
                    "Unknown Sensor", 
                    0, 
                    0, 
                    false, 
                    0, 
                    Domain.Model.ValueObjects.ESensorType.Temperature
                );

            return new AlertRestaurantResource(
                alert.Id,
                severity,
                alert.Detail,
                alert.Date,
                status,
                "restaurant",
                sensorResource
            );
        }
        else if (alert is AlertSupplier alertSupplier)
        {
            return new AlertSupplierResource(
                alert.Id,
                severity,
                alert.Detail,
                alert.Date,
                status,
                "supplier"
            );
        }

        return new AlertResource(
            alert.Id,
            severity,
            alert.Detail,
            alert.Date,
            status,
            "unknown"
        );
    }
}
