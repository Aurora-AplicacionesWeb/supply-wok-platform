namespace Aurora.SupplyWok.Platform.Operations.Interfaces.Acl;

/// <summary>
/// Facade for the Operations context
/// </summary>
public interface IOperationsContextFacade
{
    Task<WeeklyConsumptionDto> GetWeeklyConsumptionAsync(int weeks = 6, CancellationToken cancellationToken = default);
}