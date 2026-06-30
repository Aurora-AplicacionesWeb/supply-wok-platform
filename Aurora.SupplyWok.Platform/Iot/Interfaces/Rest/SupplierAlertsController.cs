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
/// REST controller for supplier alert endpoints.
/// </summary>
[ApiController]
[Route("api/v1/supplier/alerts")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Supplier Alert Endpoints.")]
public class SupplierAlertsController(
    IAlertCommandService alertCommandService,
    IAlertQueryService alertQueryService) : ControllerBase
{
    /// <summary>
    /// Gets all supplier alerts.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The supplier alerts.</returns>
    [HttpGet]
    [SwaggerOperation("Get All Supplier Alerts", "Gets all available supplier alerts.", OperationId = "GetAllSupplierAlerts")]
    [SwaggerResponse(200, "Supplier alerts retrieved successfully.", typeof(IEnumerable<AlertSupplierResource>))]
    public async Task<IActionResult> GetAllSupplierAlerts(CancellationToken cancellationToken)
    {
        var query = new GetAllSupplierAlertsQuery();
        var alerts = await alertQueryService.Handle(query, cancellationToken);
        var resources = alerts.Select(a => (AlertSupplierResource)Transform.AlertResourceFromEntityAssembler.ToResourceFromEntity(a));
        return Ok(resources);
    }

    /// <summary>
    /// Gets a supplier alert by id.
    /// </summary>
    /// <param name="alertId">The alert identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The supplier alert.</returns>
    [HttpGet("{alertId:int}")]
    [SwaggerOperation("Get Supplier Alert by Id", "Gets a supplier alert by its unique identifier.", OperationId = "GetSupplierAlertById")]
    [SwaggerResponse(200, "The alert was found and returned.", typeof(AlertSupplierResource))]
    [SwaggerResponse(404, "The alert was not found.")]
    public async Task<IActionResult> GetSupplierAlertById(int alertId, CancellationToken cancellationToken)
    {
        var query = new GetAlertByIdQuery(alertId);
        var alert = await alertQueryService.Handle(query, cancellationToken);

        if (alert == null || alert is not Domain.Model.Entities.AlertSupplier)
            return NotFound();

        return Ok((AlertSupplierResource)Transform.AlertResourceFromEntityAssembler.ToResourceFromEntity(alert));
    }

    /// <summary>
    /// Acknowledges a supplier alert.
    /// </summary>
    /// <param name="alertId">The alert identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The acknowledged supplier alert.</returns>
    [HttpPost("{alertId:int}/acknowledge")]
    [SwaggerOperation("Acknowledge Supplier Alert", "Acknowledges a supplier alert by setting its status to Acknowledged.", OperationId = "AcknowledgeSupplierAlert")]
    [SwaggerResponse(200, "The alert was acknowledged successfully.", typeof(AlertSupplierResource))]
    [SwaggerResponse(400, "Invalid request.")]
    public async Task<IActionResult> AcknowledgeSupplierAlert(int alertId, CancellationToken cancellationToken)
    {
        var query = new GetAlertByIdQuery(alertId);
        var alert = await alertQueryService.Handle(query, cancellationToken);

        if (alert == null || alert is not Domain.Model.Entities.AlertSupplier)
            return NotFound();

        var command = new AcknowledgeAlertCommand(alertId);
        var result = await alertCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok((AlertSupplierResource)Transform.AlertResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }
}
