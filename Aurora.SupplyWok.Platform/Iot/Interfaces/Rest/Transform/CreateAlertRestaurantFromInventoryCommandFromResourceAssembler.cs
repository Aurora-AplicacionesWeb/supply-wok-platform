using Aurora.SupplyWok.Platform.Iot.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Iot.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Iot.Interfaces.Rest.Transform;

/// <summary>
/// Assembler to convert a create alert from inventory resource into a command.
/// </summary>
public static class CreateAlertRestaurantFromInventoryCommandFromResourceAssembler
{
    /// <summary>
    /// Converts the resource into a create alert from inventory command.
    /// </summary>
    /// <param name="resource">The create alert from inventory resource.</param>
    /// <returns>The create alert from inventory command.</returns>
    public static CreateAlertRestaurantFromInventoryCommand ToCommandFromResource(
        CreateAlertRestaurantFromInventoryResource resource)
    {
        return new CreateAlertRestaurantFromInventoryCommand(resource.SensorId);
    }
}
