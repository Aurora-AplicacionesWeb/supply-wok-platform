using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Inventory.Domain.Model.Entities;

/// <summary>
/// Represents a stock movement for a supply item.
/// </summary>
public class StockMovement
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StockMovement"/> entity with default values.
    /// </summary>
    public StockMovement()
    {
        Reason = string.Empty;
        Date = DateTime.UtcNow;
        Supply = null!;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StockMovement"/> entity.
    /// </summary>
    /// <param name="supplyId">The supply identifier.</param>
    /// <param name="type">The movement type.</param>
    /// <param name="amount">The movement amount.</param>
    /// <param name="date">The movement date.</param>
    /// <param name="reason">The movement reason.</param>
    public StockMovement(int supplyId, EMovementType type, int amount, DateTime date, string reason) : this()
    {
        if (supplyId <= 0)
            throw new ArgumentException("Supply id must be greater than zero.", nameof(supplyId));
        if (amount <= 0)
            throw new ArgumentException("Movement amount must be greater than zero.", nameof(amount));
        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Movement reason cannot be empty.", nameof(reason));

        SupplyId = supplyId;
        Type = type;
        Amount = amount;
        Date = date;
        Reason = reason.Trim();
    }

    /// <summary>
    /// Gets the stock movement identifier.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Gets the supply identifier.
    /// </summary>
    public int SupplyId { get; private set; }

    /// <summary>
    /// Gets the movement type.
    /// </summary>
    public EMovementType Type { get; private set; }

    /// <summary>
    /// Gets the movement amount.
    /// </summary>
    public int Amount { get; private set; }

    /// <summary>
    /// Gets the movement date.
    /// </summary>
    public DateTime Date { get; private set; }

    /// <summary>
    /// Gets the movement reason.
    /// </summary>
    public string Reason { get; private set; }

    /// <summary>
    /// Gets the related supply.
    /// </summary>
    public Supply Supply { get; private set; }
}
