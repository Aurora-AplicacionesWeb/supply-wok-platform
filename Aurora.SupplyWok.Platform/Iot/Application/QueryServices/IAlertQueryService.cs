using Aurora.SupplyWok.Platform.Iot.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Iot.Domain.Model.Queries;

namespace Aurora.SupplyWok.Platform.Iot.Application.QueryServices;

/// <summary>
/// Alert query service interface.
/// </summary>
public interface IAlertQueryService
{
    /// <summary>
    /// Handles retrieving all alerts.
    /// </summary>
    /// <param name="query">The query object</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>A collection of Alerts</returns>
    Task<IEnumerable<Alert>> Handle(GetAllAlertsQuery query, CancellationToken cancellationToken);

    /// <summary>
    /// Handles retrieving all restaurant alerts.
    /// </summary>
    /// <param name="query">The query object</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>A collection of restaurant alerts</returns>
    Task<IEnumerable<Alert>> Handle(GetAllRestaurantAlertsQuery query, CancellationToken cancellationToken);

    /// <summary>
    /// Handles retrieving all supplier alerts.
    /// </summary>
    /// <param name="query">The query object</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>A collection of supplier alerts</returns>
    Task<IEnumerable<Alert>> Handle(GetAllSupplierAlertsQuery query, CancellationToken cancellationToken);

    /// <summary>
    /// Handles retrieving an alert by its identifier.
    /// </summary>
    /// <param name="query">The query object</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The Alert entity if found, otherwise null</returns>
    Task<Alert?> Handle(GetAlertByIdQuery query, CancellationToken cancellationToken);
}
