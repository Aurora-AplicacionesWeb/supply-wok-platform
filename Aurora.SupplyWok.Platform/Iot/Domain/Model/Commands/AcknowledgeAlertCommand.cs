namespace Aurora.SupplyWok.Platform.Iot.Domain.Model.Commands;

/// <summary>
/// Command to acknowledge an existing alert
/// </summary>
/// <param name="AlertId">The identifier of the alert to acknowledge</param>
public record AcknowledgeAlertCommand(int AlertId);
