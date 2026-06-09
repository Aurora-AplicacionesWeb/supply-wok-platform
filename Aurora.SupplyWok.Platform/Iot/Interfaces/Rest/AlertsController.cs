using System.Net.Mime;
using Aurora.SupplyWok.Platform.Iot.Application.CommandServices;
using Aurora.SupplyWok.Platform.Iot.Application.QueryServices;
using Aurora.SupplyWok.Platform.Iot.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Iot.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Iot.Interfaces.Rest.Resources;
using Aurora.SupplyWok.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Aurora.SupplyWok.Platform.Shared.Resources.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace Aurora.SupplyWok.Platform.Iot.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Alert Endpoints.")]
public class AlertsController(
    IAlertCommandService alertCommandService,
    IAlertQueryService alertQueryService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    [HttpPost("restaurant")]
    [SwaggerOperation("Create Restaurant Alert", "Creates a new restaurant alert triggered by a sensor.", OperationId = "CreateRestaurantAlert")]
    [SwaggerResponse(201, "The alert was created successfully.", typeof(AlertResource))]
    [SwaggerResponse(400, "Invalid request.")]
    public async Task<IActionResult> CreateRestaurantAlert([FromBody] CreateAlertRestaurantResource resource, CancellationToken cancellationToken)
    {
        var command = Transform.CreateAlertRestaurantCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await alertCommandService.Handle(command, cancellationToken);
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        var alertResource = Transform.AlertResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(nameof(GetAlertById), new { alertId = alertResource.Id }, alertResource);
    }

    [HttpGet]
    [SwaggerOperation("Get All Alerts", "Gets all available alerts (Restaurant and Supplier polymorphic list).", OperationId = "GetAllAlerts")]
    [SwaggerResponse(200, "Alerts retrieved successfully.", typeof(IEnumerable<AlertResource>))]
    public async Task<IActionResult> GetAllAlerts(CancellationToken cancellationToken)
    {
        var query = new GetAllAlertsQuery();
        var alerts = await alertQueryService.Handle(query, cancellationToken);
        var resources = alerts.Select(Transform.AlertResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{alertId:int}")]
    [SwaggerOperation("Get Alert by Id", "Get an alert by its unique identifier.", OperationId = "GetAlertById")]
    [SwaggerResponse(200, "The alert was found and returned.", typeof(AlertResource))]
    [SwaggerResponse(404, "The alert was not found.")]
    public async Task<IActionResult> GetAlertById(int alertId, CancellationToken cancellationToken)
    {
        var query = new GetAlertByIdQuery(alertId);
        var alert = await alertQueryService.Handle(query, cancellationToken);

        if (alert == null) return NotFound();
        return Ok(Transform.AlertResourceFromEntityAssembler.ToResourceFromEntity(alert));
    }

    [HttpPost("{alertId:int}/acknowledge")]
    [SwaggerOperation("Acknowledge Alert via Post", "Acknowledges an alert by setting its status to Acknowledged.", OperationId = "AcknowledgeAlertPost")]
    [SwaggerResponse(200, "The alert was acknowledged successfully.", typeof(AlertResource))]
    [SwaggerResponse(400, "Invalid request.")]
    public async Task<IActionResult> AcknowledgeAlertPost(int alertId, CancellationToken cancellationToken)
    {
        var command = new AcknowledgeAlertCommand(alertId);
        var result = await alertCommandService.Handle(command, cancellationToken);
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
            
        return Ok(Transform.AlertResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }

    [HttpPut("{alertId:int}")]
    [SwaggerOperation("Acknowledge Alert via Put", "Updates/acknowledges an existing alert.", OperationId = "AcknowledgeAlertPut")]
    [SwaggerResponse(200, "The alert was updated/acknowledged successfully.", typeof(AlertResource))]
    [SwaggerResponse(400, "Invalid request.")]
    public async Task<IActionResult> AcknowledgeAlertPut(int alertId, CancellationToken cancellationToken)
    {
        var command = new AcknowledgeAlertCommand(alertId);
        var result = await alertCommandService.Handle(command, cancellationToken);
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
            
        return Ok(Transform.AlertResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }
}
