using Aurora.SupplyWok.Platform.Operations.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Operations.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Operations.Interfaces.Rest.Transform;

public static class CreateKitchenOrderCommandFromResourceAssembler
{
    public static CreateKitchenOrderCommand ToCommandFromResource(CreateKitchenOrderResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource));
        return new CreateKitchenOrderCommand(
            resource.Number,
            resource.TableId,
            resource.TypeService,
            resource.Observations,
            resource.DateCreated);
    }
}
