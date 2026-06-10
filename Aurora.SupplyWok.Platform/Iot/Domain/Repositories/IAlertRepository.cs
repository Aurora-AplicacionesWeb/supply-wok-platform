using Aurora.SupplyWok.Platform.Iot.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Iot.Domain.Repositories;

/// <summary>
/// Represents the repository interface for Alert entities in the IoT bounded context.
/// </summary>
public interface IAlertRepository : IBaseRepository<Alert>
{
    /// <summary>
    /// Find an alert by id
    /// </summary>
    /// <param name="id">The id of the alert to search for</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The <see cref="Alert"/> if found, otherwise null</returns>
    Task<Alert?> GetAlertByIdAsync(int id, CancellationToken cancellationToken);
}
