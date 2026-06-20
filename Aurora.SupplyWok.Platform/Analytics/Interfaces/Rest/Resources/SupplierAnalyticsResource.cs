using Aurora.SupplyWok.Platform.Analytics.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Analytics.Interfaces.Rest.Resources;

public record SupplierAnalyticsResource(
    int Id,
    ICollection<SupplierAggregatePeriod> Aggregate,
    ICollection<SupplierClientDemand> Clients
);
