using System.Net.Mime;
using Aurora.SupplyWok.Platform.Iot.Application.CommandServices;
using Aurora.SupplyWok.Platform.Iot.Application.QueryServices;
using Aurora.SupplyWok.Platform.Iot.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Iot.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Iot.Interfaces.Rest.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Aurora.SupplyWok.Platform.Iot.Interfaces.Rest;

/// <summary>
/// REST controller for restaurant alert endpoints.
/// </summary>
[ApiController]
[Route("api/v1/restaurant/alerts")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Restaurant Alert Endpoints.")]
public class RestaurantAlertsController(
    IAlertCommandService alertCommandService,
    IAlertQueryService alertQueryService) : ControllerBase
{


    /// <summary>
    /// Gets all restaurant alerts.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The restaurant alerts.</returns>
    [HttpGet]
    [SwaggerOperation("Get All Restaurant Alerts", "Gets all available restaurant alerts.", OperationId = "GetAllRestaurantAlerts")]
    [SwaggerResponse(200, "Restaurant alerts retrieved successfully.", typeof(IEnumerable<AlertRestaurantResource>))]
    public async Task<IActionResult> GetAllRestaurantAlerts(CancellationToken cancellationToken)
    {
        var query = new GetAllRestaurantAlertsQuery();
        var alerts = await alertQueryService.Handle(query, cancellationToken);
        var resources = alerts.Select(a => (AlertRestaurantResource)Transform.AlertResourceFromEntityAssembler.ToResourceFromEntity(a));
        return Ok(resources);
    }

    /// <summary>
    /// Gets a restaurant alert by id.
    /// </summary>
    /// <param name="alertId">The alert identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The restaurant alert.</returns>
    [HttpGet("{alertId:int}")]
    [SwaggerOperation("Get Restaurant Alert by Id", "Gets a restaurant alert by its unique identifier.", OperationId = "GetRestaurantAlertById")]
    [SwaggerResponse(200, "The alert was found and returned.", typeof(AlertRestaurantResource))]
    [SwaggerResponse(404, "The alert was not found.")]
    public async Task<IActionResult> GetRestaurantAlertById(int alertId, CancellationToken cancellationToken)
    {
        var query = new GetAlertByIdQuery(alertId);
        var alert = await alertQueryService.Handle(query, cancellationToken);

        if (alert == null || alert is not Domain.Model.Entities.AlertRestaurant)
            return NotFound();

        return Ok((AlertRestaurantResource)Transform.AlertResourceFromEntityAssembler.ToResourceFromEntity(alert));
    }

    /// <summary>
    /// Acknowledges a restaurant alert.
    /// </summary>
    /// <param name="alertId">The alert identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The acknowledged restaurant alert.</returns>
    [HttpPost("{alertId:int}/acknowledge")]
    [SwaggerOperation("Acknowledge Restaurant Alert", "Acknowledges a restaurant alert by setting its status to Acknowledged.", OperationId = "AcknowledgeRestaurantAlert")]
    [SwaggerResponse(200, "The alert was acknowledged successfully.", typeof(AlertRestaurantResource))]
    [SwaggerResponse(400, "Invalid request.")]
    public async Task<IActionResult> AcknowledgeRestaurantAlert(int alertId, CancellationToken cancellationToken)
    {
        var query = new GetAlertByIdQuery(alertId);
        var alert = await alertQueryService.Handle(query, cancellationToken);

        if (alert == null || alert is not Domain.Model.Entities.AlertRestaurant)
            return NotFound();

        var command = new AcknowledgeAlertCommand(alertId);
        var result = await alertCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok((AlertRestaurantResource)Transform.AlertResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }
}
