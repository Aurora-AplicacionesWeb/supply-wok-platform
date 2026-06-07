using Aurora.SupplyWok.Platform.Iot.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Iot.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Shared.Application.Model;

namespace Aurora.SupplyWok.Platform.Iot.Application.CommandServices;

/// <summary>
/// Sensor command service interface
/// </summary>
public interface ISensorCommandService
{
    /// <summary>
    /// Handle create sensor command
    /// </summary>
    /// <param name="command">The <see cref="CreateSensorCommand"/> command</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The <see cref="Sensor"/> object with the created sensor</returns>
    Task<Result<Sensor>> Handle(CreateSensorCommand command, CancellationToken cancellationToken);

    /// <summary>
    /// Handle update sensor command
    /// </summary>
    /// <param name="command">The <see cref="UpdateSensorCommand"/> command</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The <see cref="Sensor"/> object with the updated sensor</returns>
    Task<Result<Sensor>> Handle(UpdateSensorCommand command, CancellationToken cancellationToken);

    /// <summary>
    /// Handle delete sensor command
    /// </summary>
    /// <param name="command">The <see cref="DeleteSensorCommand"/> command</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>True if deleted successfully</returns>
    Task<Result<bool>> Handle(DeleteSensorCommand command, CancellationToken cancellationToken);
}