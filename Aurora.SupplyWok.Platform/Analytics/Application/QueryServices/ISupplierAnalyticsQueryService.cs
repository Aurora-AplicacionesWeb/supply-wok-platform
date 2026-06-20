using Aurora.SupplyWok.Platform.Analytics.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Analytics.Domain.Model.Queries;

namespace Aurora.SupplyWok.Platform.Analytics.Application.QueryServices;

public interface ISupplierAnalyticsQueryService
{
    Task<IEnumerable<SupplierAnalytics>> Handle(GetAllSupplierAnalyticsQuery query, CancellationToken cancellationToken);
}
