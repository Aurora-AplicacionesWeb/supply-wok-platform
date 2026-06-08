using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Purchasing.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Purchasing.Interfaces.Rest.Transform;

public static class UpdatePurchaseOrderStatusCommandFromResourceAssembler
{
    public static UpdatePurchaseOrderStatusCommand ToCommandFromResource(int id, UpdatePurchaseOrderStatusResource resource)
    {
        ArgumentNullException.ThrowIfNull(resource);
        return new UpdatePurchaseOrderStatusCommand(id, resource.Status);
    }
}
