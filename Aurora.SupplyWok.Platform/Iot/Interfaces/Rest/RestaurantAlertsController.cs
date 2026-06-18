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
    /// Creates a restaurant alert triggered by a sensor.
    /// </summary>
    /// <param name="resource">The resource with restaurant alert data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created restaurant alert.</returns>
    [HttpPost]
    [SwaggerOperation("Create Restaurant Alert", "Creates a new restaurant alert triggered by a sensor.", OperationId = "CreateRestaurantAlert")]
    [SwaggerResponse(201, "The alert was created successfully.", typeof(AlertResource))]
    [SwaggerResponse(400, "Invalid request.")]
    public async Task<IActionResult> CreateRestaurantAlert([FromBody] CreateAlertRestaurantResource resource,
        CancellationToken cancellationToken)
    {
        var command = Transform.CreateAlertRestaurantCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await alertCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        var alertResource = Transform.AlertResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(nameof(GetRestaurantAlertById), new { alertId = alertResource.Id }, alertResource);
    }

    /// <summary>
    /// Creates a restaurant alert when the inventory stock differs from the last sensor value.
    /// </summary>
    /// <param name="resource">The resource with the sensor identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created alert, or no content when inventory and sensor values are equal.</returns>
    [HttpPost("inventory")]
    [SwaggerOperation("Create Restaurant Alert From Inventory", "Creates a restaurant alert if current inventory stock differs from the last sensor value.", OperationId = "CreateRestaurantAlertFromInventory")]
    [SwaggerResponse(201, "The alert was created successfully.", typeof(AlertResource))]
    [SwaggerResponse(204, "No alert was created because inventory and sensor values match.")]
    [SwaggerResponse(400, "Invalid request.")]
    public async Task<IActionResult> CreateRestaurantAlertFromInventory(
        [FromBody] CreateAlertRestaurantFromInventoryResource resource,
        CancellationToken cancellationToken)
    {
        var command = Transform.CreateAlertRestaurantFromInventoryCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await alertCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        if (result.Value == null)
            return NoContent();

        var alertResource = Transform.AlertResourceFromEntityAssembler.ToResourceFromEntity(result.Value);
        return CreatedAtAction(nameof(GetRestaurantAlertById), new { alertId = alertResource.Id }, alertResource);
    }

    /// <summary>
    /// Gets all restaurant alerts.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The restaurant alerts.</returns>
    [HttpGet]
    [SwaggerOperation("Get All Restaurant Alerts", "Gets all available restaurant alerts.", OperationId = "GetAllRestaurantAlerts")]
    [SwaggerResponse(200, "Restaurant alerts retrieved successfully.", typeof(IEnumerable<AlertResource>))]
    public async Task<IActionResult> GetAllRestaurantAlerts(CancellationToken cancellationToken)
    {
        var query = new GetAllRestaurantAlertsQuery();
        var alerts = await alertQueryService.Handle(query, cancellationToken);
        var resources = alerts.Select(Transform.AlertResourceFromEntityAssembler.ToResourceFromEntity);
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
    [SwaggerResponse(200, "The alert was found and returned.", typeof(AlertResource))]
    [SwaggerResponse(404, "The alert was not found.")]
    public async Task<IActionResult> GetRestaurantAlertById(int alertId, CancellationToken cancellationToken)
    {
        var query = new GetAlertByIdQuery(alertId);
        var alert = await alertQueryService.Handle(query, cancellationToken);

        if (alert == null || alert is not Domain.Model.Entities.AlertRestaurant)
            return NotFound();

        return Ok(Transform.AlertResourceFromEntityAssembler.ToResourceFromEntity(alert));
    }

    /// <summary>
    /// Acknowledges a restaurant alert.
    /// </summary>
    /// <param name="alertId">The alert identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The acknowledged restaurant alert.</returns>
    [HttpPost("{alertId:int}/acknowledge")]
    [SwaggerOperation("Acknowledge Restaurant Alert", "Acknowledges a restaurant alert by setting its status to Acknowledged.", OperationId = "AcknowledgeRestaurantAlert")]
    [SwaggerResponse(200, "The alert was acknowledged successfully.", typeof(AlertResource))]
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

        return Ok(Transform.AlertResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }
}
