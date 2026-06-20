namespace Aurora.SupplyWok.Platform.Analytics.Domain.Model.ValueObjects;

public record TrendData(List<string> Labels, List<int> Data)
{
    public TrendData() : this(new List<string>(), new List<int>()) {}
}
