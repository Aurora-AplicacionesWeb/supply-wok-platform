namespace Aurora.SupplyWok.Platform.Iot.Domain.Model.Queries;

/// <summary>
/// Query to retrieve a specific alert by its identifier.
/// </summary>
/// <param name="AlertId">The identifier of the alert to retrieve</param>
public record GetAlertByIdQuery(int AlertId);
