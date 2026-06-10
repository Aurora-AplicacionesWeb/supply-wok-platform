using Aurora.SupplyWok.Platform.Iot.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Iot.Domain.Model.ValueObjects;
using Aurora.SupplyWok.Platform.Iot.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Iot.Interfaces.Rest.Transform;

/// <summary>
/// Assembler to convert CreateAlertRestaurantResource to CreateAlertRestaurantCommand.
/// </summary>
public static class CreateAlertRestaurantCommandFromResourceAssembler
{
    /// <summary>
    /// Convert a CreateAlertRestaurantResource to CreateAlertRestaurantCommand.
    /// </summary>
    /// <param name="resource">The resource to convert</param>
    /// <returns>A new CreateAlertRestaurantCommand instance</returns>
    /// <exception cref="ArgumentNullException">Thrown when the resource is null</exception>
    public static CreateAlertRestaurantCommand ToCommandFromResource(CreateAlertRestaurantResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource),
                "CreateAlertRestaurantResource cannot be null when converting to command.");

        if (!Enum.TryParse<EAlertSeverity>(resource.Severity, true, out var severity))
        {
            severity = EAlertSeverity.Low;
        }

        return new CreateAlertRestaurantCommand(
            severity,
            resource.Detail,
            resource.SensorId
        );
    }
}
