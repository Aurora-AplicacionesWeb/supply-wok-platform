using Aurora.SupplyWok.Platform.Operations.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Operations.Interfaces.Rest.Resources;


namespace Aurora.SupplyWok.Platform.Operations.Interfaces.Rest.Transform;

public static class UpdateTableCommandFromResourceAssembler
{
    public static UpdateTableCommand ToCommandFromResource(int id, UpdateTableResource resource)
    {
        return new UpdateTableCommand(id, resource.Number, resource.Capacity, resource.Location, resource.State, resource.Active);
    }
}