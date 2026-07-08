using Aurora.SupplyWok.Platform.Analytics.Application.QueryServices;
using Aurora.SupplyWok.Platform.Analytics.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Analytics.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Analytics.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Analytics.Application.Internal.QueryServices;

public class SupplierAnalyticsQueryService(ISupplierAnalyticsRepository repository) : ISupplierAnalyticsQueryService
{
    public async Task<IEnumerable<SupplierAnalytics>> Handle(GetAllSupplierAnalyticsQuery query, CancellationToken cancellationToken)
    {
        var results = await repository.ListAsync(cancellationToken);

        if (query.ClientId.HasValue)
        {
            results = results
                .Select(sa => new SupplierAnalytics(
                    sa.Aggregate,
                    sa.Clients.Where(c => c.ClientId == query.ClientId.Value).ToList()
                ))
                .ToList();
        }

        return results;
    }
}
