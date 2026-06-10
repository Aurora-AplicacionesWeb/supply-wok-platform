using Aurora.SupplyWok.Platform.Iot.Application.QueryServices;
using Aurora.SupplyWok.Platform.Iot.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Iot.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Iot.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Iot.Application.Internal.QueryServices;

/// <summary>
/// Alert query service implementation.
/// </summary>
/// <param name="alertRepository">Alert repository</param>
public class AlertQueryService(IAlertRepository alertRepository) : IAlertQueryService
{
    // <inheritdoc />
    public async Task<IEnumerable<Alert>> Handle(GetAllAlertsQuery query, CancellationToken cancellationToken)
    {
        return await alertRepository.ListAsync(cancellationToken);
    }

    // <inheritdoc />
    public async Task<Alert?> Handle(GetAlertByIdQuery query, CancellationToken cancellationToken)
    {
        return await alertRepository.GetAlertByIdAsync(query.AlertId, cancellationToken);
    }
}
