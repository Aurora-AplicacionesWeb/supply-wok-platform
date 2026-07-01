using Aurora.SupplyWok.Platform.Spm.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;

/// <summary>
///     Catalog item aggregate owned by a supplier.
/// </summary>
public partial class CatalogItem
{
    /// <summary>
    ///     Initializes a new catalog item instance with business data.
    /// </summary>
    public CatalogItem(int supplierId, string name, string category, decimal price, ECatalogUnit unit, string deliveryConditions) : this()
    {
        SetSupplierId(supplierId);
        UpdateCore(name, category, price, unit, deliveryConditions);
    }

    /// <summary>
    ///     Initializes a new catalog item instance from a create command.
    /// </summary>
    public CatalogItem(CreateCatalogItemCommand command) : this(
        command.SupplierId,
        command.Name,
        command.Category,
        command.Price,
        command.Unit,
        command.DeliveryConditions)
    {
    }

    public int Id { get; private set; }

    public int SupplierId { get; private set; }

    public string Name { get; private set; }

    public string Category { get; private set; }

    public decimal Price { get; private set; }

    public ECatalogUnit Unit { get; private set; }

    public string DeliveryConditions { get; private set; }

    public void Update(string name, string category, decimal price, ECatalogUnit unit, string deliveryConditions)
    {
        UpdateCore(name, category, price, unit, deliveryConditions);
    }

    private void SetSupplierId(int supplierId)
    {
        if (supplierId <= 0)
            throw new ArgumentException("Supplier id must be greater than zero.", nameof(supplierId));

        SupplierId = supplierId;
    }

    private void UpdateCore(string name, string category, decimal price, ECatalogUnit unit, string deliveryConditions)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Catalog item name cannot be empty.", nameof(name));
        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentException("Catalog item category cannot be empty.", nameof(category));
        if (price <= 0)
            throw new ArgumentException("Catalog item price must be greater than zero.", nameof(price));
        if (string.IsNullOrWhiteSpace(deliveryConditions))
            throw new ArgumentException("Delivery conditions cannot be empty.", nameof(deliveryConditions));

        Name = name;
        Category = category;
        Price = decimal.Round(price, 2, MidpointRounding.AwayFromZero);
        Unit = unit;
        DeliveryConditions = deliveryConditions;
    }
}
