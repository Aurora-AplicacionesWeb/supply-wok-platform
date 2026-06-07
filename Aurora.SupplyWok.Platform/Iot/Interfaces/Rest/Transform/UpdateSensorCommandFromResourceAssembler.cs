using Aurora.SupplyWok.Platform.Iot.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Iot.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Iot.Interfaces.Rest.Transform;

public static class UpdateSensorCommandFromResourceAssembler
{
    public static UpdateSensorCommand ToCommandFromResource(int id, UpdateSensorResource resource)
    {
        return new UpdateSensorCommand(id, resource.Name, resource.MinValue, resource.MaxValue, resource.Enabled, resource.LastValue, resource.Type);
    }
}
