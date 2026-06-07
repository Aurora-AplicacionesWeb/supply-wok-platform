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
[SwaggerTag("Available Sensor Endpoints.")]
public class SensorsController(
    ISensorCommandService sensorCommandService,
    ISensorQueryService sensorQueryService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;
    
    [HttpPost]
    [SwaggerOperation("Create Sensor", "Creates a new sensor.", OperationId = "CreateSensor")]
    [SwaggerResponse(201, "The sensor was created successfully.", typeof(SensorResource))]
    [SwaggerResponse(400, "Invalid request.")]
    public async Task<IActionResult> CreateSensor([FromBody] CreateSensorResource resource, CancellationToken cancellationToken)
    {
        var command = Transform.CreateSensorCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await sensorCommandService.Handle(command, cancellationToken);
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        var sensorResource = Transform.SensorResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(nameof(GetSensorById), new { sensorId = sensorResource.Id }, sensorResource);
    }

    [HttpGet]
    [SwaggerOperation("Get All Sensors", "Gets all available sensors.", OperationId = "GetAllSensors")]
    [SwaggerResponse(200, "Sensors retrieved successfully.", typeof(IEnumerable<SensorResource>))]
    public async Task<IActionResult> GetAllSensors(CancellationToken cancellationToken)
    {
        var query = new GetAllSensorsQuery();
        var sensors = await sensorQueryService.Handle(query, cancellationToken);
        var resources = sensors.Select(Transform.SensorResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{sensorId:int}")]
    [SwaggerOperation("Get Sensor by Id", "Get a sensor by its unique identifier.", OperationId = "GetSensorById")]
    [SwaggerResponse(200, "The sensor was found and returned.", typeof(SensorResource))]
    [SwaggerResponse(404, "The sensor was not found.")]
    public async Task<IActionResult> GetSensorById(int sensorId, CancellationToken cancellationToken)
    {
        var getSensorByIdQuery = new GetSensorByIdQuery(sensorId);
        var sensor = await sensorQueryService.Handle(getSensorByIdQuery, cancellationToken);

        if (sensor == null) return NotFound();
        return Ok(Transform.SensorResourceFromEntityAssembler.ToResourceFromEntity(sensor));
    }

    [HttpPut("{sensorId:int}")]
    [SwaggerOperation("Update Sensor", "Updates an existing sensor.", OperationId = "UpdateSensor")]
    [SwaggerResponse(200, "The sensor was updated successfully.", typeof(SensorResource))]
    [SwaggerResponse(400, "Invalid request.")]
    public async Task<IActionResult> UpdateSensor(int sensorId, [FromBody] UpdateSensorResource resource, CancellationToken cancellationToken)
    {
        var command = Transform.UpdateSensorCommandFromResourceAssembler.ToCommandFromResource(sensorId, resource);
        var result = await sensorCommandService.Handle(command, cancellationToken);
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
            
        return Ok(Transform.SensorResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }

    [HttpDelete("{sensorId:int}")]
    [SwaggerOperation("Delete Sensor", "Deletes an existing sensor.", OperationId = "DeleteSensor")]
    [SwaggerResponse(204, "The sensor was deleted successfully.")]
    [SwaggerResponse(400, "Invalid request.")]
    public async Task<IActionResult> DeleteSensor(int sensorId, CancellationToken cancellationToken)
    {
        var command = new DeleteSensorCommand(sensorId);
        var result = await sensorCommandService.Handle(command, cancellationToken);
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
            
        return NoContent();
    }
}