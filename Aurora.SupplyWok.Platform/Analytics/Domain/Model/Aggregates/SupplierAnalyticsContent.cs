using Aurora.SupplyWok.Platform.Analytics.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Analytics.Domain.Model.Aggregates;

public partial class SupplierAnalytics
{
    public SupplierAnalytics()
    {
        Aggregate = new List<SupplierAggregatePeriod>();
        Clients = new List<SupplierClientDemand>();
    }
}
