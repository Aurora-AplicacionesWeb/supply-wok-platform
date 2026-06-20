namespace Aurora.SupplyWok.Platform.Analytics.Domain.Model.ValueObjects;

public record SupplierAggregatePeriod(string Period, int Value)
{
    public SupplierAggregatePeriod() : this(string.Empty, 0) {}
}
