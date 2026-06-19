using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Inventory.Domain.Model.Entities;

public class InventoryTransaction
{
    private readonly List<InventoryOperation> _operations = [];

    public InventoryTransaction()
    {
        Reason = string.Empty;
        TransactionDate = DateTime.UtcNow;
        Supply = null!;
    }

    public InventoryTransaction(int supplyId, EInventoryTransactionType type, int amount, DateTime transactionDate, string reason) : this()
    {
        if (supplyId <= 0)
            throw new ArgumentException("Supply id must be greater than zero.", nameof(supplyId));
        if (amount <= 0)
            throw new ArgumentException("Transaction amount must be greater than zero.", nameof(amount));
        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Transaction reason cannot be empty.", nameof(reason));

        SupplyId = supplyId;
        Type = type;
        Amount = amount;
        TransactionDate = transactionDate;
        Reason = reason.Trim();
    }

    public int Id { get; private set; }
    public int SupplyId { get; private set; }
    public EInventoryTransactionType Type { get; private set; }
    public int Amount { get; private set; }
    public DateTime TransactionDate { get; private set; }
    public string Reason { get; private set; }
    public Supply Supply { get; private set; }

    public IReadOnlyCollection<InventoryOperation> Operations => _operations.AsReadOnly();

    public InventoryOperation AddOperation(EInventoryOperationType type, int amount, string? notes = null)
    {
        if (amount <= 0)
            throw new ArgumentException("Operation amount must be greater than zero.", nameof(amount));

        var operation = new InventoryOperation(type, amount, DateTime.UtcNow, notes);
        _operations.Add(operation);
        return operation;
    }
}
