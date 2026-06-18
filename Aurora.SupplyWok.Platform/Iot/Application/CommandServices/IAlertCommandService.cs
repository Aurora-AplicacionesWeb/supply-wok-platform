using Aurora.SupplyWok.Platform.Iot.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Iot.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Shared.Application.Model;

namespace Aurora.SupplyWok.Platform.Iot.Application.CommandServices;

/// <summary>
/// Alert command service interface.
/// </summary>
public interface IAlertCommandService
{
    /// <summary>
    /// Handles the creation of a restaurant alert.
    /// </summary>
    /// <param name="command">The create alert command</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>A Result containing the created Alert</returns>
    Task<Result<Alert>> Handle(CreateAlertRestaurantCommand command, CancellationToken cancellationToken);

    /// <summary>
    /// Handles the creation of a restaurant alert from the current inventory stock.
    /// </summary>
    /// <param name="command">The create alert from inventory command</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>A Result containing the created Alert, or null when no alert is required</returns>
    Task<Result<Alert?>> Handle(CreateAlertRestaurantFromInventoryCommand command, CancellationToken cancellationToken);

    /// <summary>
    /// Handles the acknowledgement of an alert.
    /// </summary>
    /// <param name="command">The acknowledge command</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>A Result containing the acknowledged Alert</returns>
    Task<Result<Alert>> Handle(AcknowledgeAlertCommand command, CancellationToken cancellationToken);
}
