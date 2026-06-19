using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Inventory.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Inventory.Interfaces.Rest.Transform;

public static class InventoryTransactionResourceFromEntityAssembler
{
    public static InventoryTransactionResource ToResourceFromEntity(InventoryTransaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction);

        var operationResources = transaction.Operations
            .Select(o => new InventoryOperationResource(
                o.Id,
                o.Type.ToString(),
                o.Amount,
                o.OperationDate,
                o.Notes))
            .ToList();

        return new InventoryTransactionResource(
            transaction.Id,
            transaction.SupplyId,
            transaction.Type.ToString(),
            transaction.Amount,
            transaction.TransactionDate,
            transaction.Reason,
            operationResources.AsReadOnly());
    }
}
