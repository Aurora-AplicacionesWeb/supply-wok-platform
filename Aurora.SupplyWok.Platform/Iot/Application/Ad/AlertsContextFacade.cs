using Aurora.SupplyWok.Platform.Iot.Application.CommandServices;
using Aurora.SupplyWok.Platform.Iot.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Iot.Domain.Model.ValueObjects;
using Aurora.SupplyWok.Platform.Iot.Interfaces.Acl;

namespace Aurora.SupplyWok.Platform.Iot.Application.Ad;

/// <summary>
/// Facade implementation for the Alert context
/// </summary>
public class AlertsContextFacade(IAlertCommandService alertCommandService) : IAlertsContextFacade
{
    /// <inheritdoc />
    public async Task<int> CreateAlertRestaurant(string severity, string detail, int sensorId, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<EAlertSeverity>(severity, true, out var alertSeverity))
        {
            alertSeverity = EAlertSeverity.Low;
        }

        var command = new CreateAlertRestaurantCommand(alertSeverity, detail, sensorId);
        var result = await alertCommandService.Handle(command, cancellationToken);
        
        return result.IsSuccess && result.Value != null ? result.Value.Id : 0;
    }
}
