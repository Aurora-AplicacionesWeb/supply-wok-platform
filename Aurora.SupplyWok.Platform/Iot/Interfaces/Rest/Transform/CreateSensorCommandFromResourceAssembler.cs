using Aurora.SupplyWok.Platform.Iot.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Iot.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Iot.Interfaces.Rest.Transform;

public static class CreateSensorCommandFromResourceAssembler
{
    /// <summary>
    /// Convert a <see cref="CreateSensorCommand"/> to a <see cref="CreateSensorResource"/>
    /// </summary>
    /// <param name="resource">The <see cref="CreateSensorResource"/> containing the data for creating a sensor. Must not be null</param>
    /// <returns>A new <see cref="CreateSensorCommand"/> instance</returns>
    /// <exception cref="ArgumentNullException">Throw if the input <paramref name="resource"/> is null</exception>
    public static CreateSensorCommand ToCommandFromResource(CreateSensorResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource),
                "CreateSensorResource cannot be null when converting to command.");
        return new CreateSensorCommand(
            resource.Name,
            resource.MinValue,
            resource.MaxValue,
            resource.Enabled,
            resource.LastValue,
            resource.Type
        );
    }
}