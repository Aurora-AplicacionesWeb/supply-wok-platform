using Aurora.SupplyWok.Platform.Iot.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Iot.Domain.Model.Queries;

namespace Aurora.SupplyWok.Platform.Iot.Application.QueryServices;

/// <summary>
/// Sensor query service interface
/// </summary>
public interface ISensorQueryService
{
    /// <summary>
    /// Handle get all sensors
    /// </summary>
    /// <param name="query">The <see cref="GetAllSensorsQuery"/> query</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The collection of <see cref="Sensor"/> objects</returns>
    Task<IEnumerable<Sensor>> Handle(GetAllSensorsQuery query, CancellationToken cancellationToken);
    
    /// <summary>
    /// Handle get sensor by ID
    /// </summary>
    /// <param name="query">The <see cref="GetSensorByIdQuery"/> query</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The <see cref="Sensor"/> object or null if not found</returns>
    Task<Sensor?> Handle(GetSensorByIdQuery query, CancellationToken cancellationToken);
}