using Aurora.SupplyWok.Platform.Iot.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Iot.Domain.Repositories;

/// <summary>
/// Represents the Sensor repository in the Supply Wok Platform.
/// </summary>
public interface ISensorRepository : IBaseRepository<Sensor>
{
    /// <summary>
    /// Find a sensor by id
    /// </summary>
    /// <param name="id">The id of the sensor to search for</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The <see cref="Sensor"/> if found, otherwise null</returns>
    Task<Sensor?> GetSensorByIdAsync(int id, CancellationToken cancellationToken);
}