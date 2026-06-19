namespace Aurora.SupplyWok.Platform.Inventory.Interfaces.Rest.Resources;

public record CreateInventoryTransactionResource(
    int SupplyId,
    string Type,
    int Amount,
    DateTime TransactionDate,
    string Reason);
