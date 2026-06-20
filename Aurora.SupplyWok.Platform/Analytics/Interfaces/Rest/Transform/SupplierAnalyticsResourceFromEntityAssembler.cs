using Aurora.SupplyWok.Platform.Analytics.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Analytics.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Analytics.Interfaces.Rest.Transform;

public static class SupplierAnalyticsResourceFromEntityAssembler
{
    public static SupplierAnalyticsResource ToResourceFromEntity(SupplierAnalytics entity)
    {
        return new SupplierAnalyticsResource(
            entity.Id,
            entity.Aggregate,
            entity.Clients
        );
    }
}
