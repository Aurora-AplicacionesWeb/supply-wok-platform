namespace Aurora.SupplyWok.Platform.Analytics.Domain.Model.ValueObjects;

public record TopSupplierOrder(string Supplier, int Value)
{
    public TopSupplierOrder() : this(string.Empty, 0) {}
}
