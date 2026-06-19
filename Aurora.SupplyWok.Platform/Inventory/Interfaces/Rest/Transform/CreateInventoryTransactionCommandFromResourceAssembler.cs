using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.ValueObjects;
using Aurora.SupplyWok.Platform.Inventory.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Inventory.Interfaces.Rest.Transform;

public static class CreateInventoryTransactionCommandFromResourceAssembler
{
    public static CreateInventoryTransactionCommand ToCommandFromResource(CreateInventoryTransactionResource resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        return new CreateInventoryTransactionCommand(
            resource.SupplyId,
            Enum.Parse<EInventoryTransactionType>(resource.Type),
            resource.Amount,
            resource.TransactionDate,
            resource.Reason);
    }
}
