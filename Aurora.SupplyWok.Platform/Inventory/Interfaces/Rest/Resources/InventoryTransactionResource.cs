namespace Aurora.SupplyWok.Platform.Inventory.Interfaces.Rest.Resources;

public record InventoryOperationResource(
    int Id,
    string Type,
    int Amount,
    DateTime OperationDate,
    string Notes);

public record InventoryTransactionResource(
    int Id,
    int SupplyId,
    string Type,
    int Amount,
    DateTime TransactionDate,
    string Reason,
    IReadOnlyCollection<InventoryOperationResource> Operations);
