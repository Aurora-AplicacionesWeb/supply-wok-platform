using Aurora.SupplyWok.Platform.Inventory.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Inventory.Domain.Model.Commands;

/// <summary>
/// Command to create an inventory transaction with its corresponding operations.
/// </summary>
/// <param name="SupplyId">The supply identifier.</param>
/// <param name="Type">The transaction type (Entry, Exit, Transfer).</param>
/// <param name="Amount">The transaction amount.</param>
/// <param name="TransactionDate">The transaction date.</param>
/// <param name="Reason">The transaction reason.</param>
public record CreateInventoryTransactionCommand(
    int SupplyId,
    EInventoryTransactionType Type,
    int Amount,
    DateTime TransactionDate,
    string Reason);
