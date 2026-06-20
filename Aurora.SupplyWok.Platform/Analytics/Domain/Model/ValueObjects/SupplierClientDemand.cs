namespace Aurora.SupplyWok.Platform.Analytics.Domain.Model.ValueObjects;

public record SupplierClientDemand(int ClientId, string ClientName, int Value, string Trend, string Summary)
{
    public SupplierClientDemand() : this(0, string.Empty, 0, string.Empty, string.Empty) {}
}
