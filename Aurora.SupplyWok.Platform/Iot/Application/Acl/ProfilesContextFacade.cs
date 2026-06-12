using Aurora.SupplyWok.Platform.Iot.Application.CommandServices;
using Aurora.SupplyWok.Platform.Iot.Application.QueryServices;
using Aurora.SupplyWok.Platform.Iot.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Iot.Domain.Model.ValueObjects;
using Aurora.SupplyWok.Platform.Iot.Interfaces.Acl;

namespace Aurora.SupplyWok.Platform.Iot.Application.Acl;

public class ProfilesContextFacade(
    ISensorCommandService sensorCommandService,
    ISensorQueryService sensorQueryService) : ISensorContextFacade
{
    // <inheritdoc />
    public async Task<int> CreateSensor(string name,
        double minValue,
        double maxValue,
        bool enabled,
        double lastValue,
        ESensorType sensorType,
        CancellationToken cancellationToken)
    {
        var createSensorCommand = 
            new CreateSensorCommand(name, minValue, maxValue, enabled, lastValue, sensorType);
        var result = await sensorCommandService.Handle(createSensorCommand, cancellationToken);
        return result.Value?.Id ?? 0;
    }
}