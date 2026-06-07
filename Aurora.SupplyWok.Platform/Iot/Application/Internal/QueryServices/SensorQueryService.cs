using Aurora.SupplyWok.Platform.Iot.Application.QueryServices;
using Aurora.SupplyWok.Platform.Iot.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Iot.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Iot.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Iot.Application.Internal.QueryServices;

/// <summary>
/// Sensor query service
/// </summary>
/// <param name="sensorRepository">Sensor repository</param>
public class SensorQueryService(ISensorRepository sensorRepository) : ISensorQueryService
{
    // <InheritDoc/>
    public async Task<IEnumerable<Sensor>> Handle(GetAllSensorsQuery query, CancellationToken cancellationToken)
    {
        return await sensorRepository.ListAsync(cancellationToken);
    }
    
    // <InheritDoc/>
    public async Task<Sensor?> Handle(GetSensorByIdQuery query, CancellationToken cancellationToken)
    {
        return await sensorRepository.GetSensorByIdAsync(query.SensorId, cancellationToken);
    }
}