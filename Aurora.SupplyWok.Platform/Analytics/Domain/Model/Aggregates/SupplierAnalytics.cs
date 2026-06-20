using Aurora.SupplyWok.Platform.Analytics.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Analytics.Domain.Model.Aggregates;

public partial class SupplierAnalytics
{
    public SupplierAnalytics(
        ICollection<SupplierAggregatePeriod> aggregate,
        ICollection<SupplierClientDemand> clients) : this()
    {
        Aggregate = aggregate;
        Clients = clients;
    }

    public int Id { get; private set; }
    public ICollection<SupplierAggregatePeriod> Aggregate { get; private set; }
    public ICollection<SupplierClientDemand> Clients { get; private set; }
}
