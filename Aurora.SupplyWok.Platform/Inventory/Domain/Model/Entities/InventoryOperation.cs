using Aurora.SupplyWok.Platform.Inventory.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Inventory.Domain.Model.Entities;

public class InventoryOperation
{
    public InventoryOperation()
    {
        Notes = string.Empty;
        OperationDate = DateTime.UtcNow;
        InventoryTransaction = null!;
    }

    public InventoryOperation(EInventoryOperationType type, int amount, DateTime operationDate, string? notes) : this()
    {
        if (amount <= 0)
            throw new ArgumentException("Operation amount must be greater than zero.", nameof(amount));

        Type = type;
        Amount = amount;
        OperationDate = operationDate;
        Notes = notes?.Trim() ?? string.Empty;
    }

    public int Id { get; private set; }
    public int InventoryTransactionId { get; private set; }
    public EInventoryOperationType Type { get; private set; }
    public int Amount { get; private set; }
    public DateTime OperationDate { get; private set; }
    public string Notes { get; private set; }
    public InventoryTransaction InventoryTransaction { get; private set; }
}
