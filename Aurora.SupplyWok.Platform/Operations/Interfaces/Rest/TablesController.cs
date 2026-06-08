using System.Net.Mime;
using Aurora.SupplyWok.Platform.Operations.Application.CommandServices;
using Aurora.SupplyWok.Platform.Operations.Application.QueryServices;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Operations.Interfaces.Rest.Resources;
using Aurora.SupplyWok.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Aurora.SupplyWok.Platform.Shared.Resources.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
namespace Aurora.SupplyWok.Platform.Operations.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Table Endpoints.")]
public class TablesController (ITableCommandService tableCommandService, 
    ITableQueryService tableQueryService, 
    IStringLocalizer<ErrorMessages> errorLocalizer, 
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;
    
    [HttpPost]
    [SwaggerOperation("Create Table", "Creates a new table.", OperationId = "CreateTable")]
    [SwaggerResponse(201, "The table was created successfully.", typeof(TableResource))]
    [SwaggerResponse(400, "Invalid request.")]
    
    public async Task<IActionResult> CreateTable([FromBody] CreateTableResource resource, CancellationToken cancellationToken)
    {
        var command = Transform.CreateTableCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await tableCommandService.Handle(command, cancellationToken);
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        var tableResource = Transform.TableResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(nameof(GetTableById), new { tableId = tableResource.Id }, tableResource);
    }
    
    [HttpGet]
    [SwaggerOperation("Get All Tables", "Gets all available tables.", OperationId = "GetAllTables")]
    [SwaggerResponse(200, "Tables retrieved successfully.", typeof(IEnumerable<TableResource>))]
    public async Task<IActionResult> GetAllTables(CancellationToken cancellationToken)
    {
        var query = new GetAllTablesQuery();
        var tables = await tableQueryService.Handle(query, cancellationToken);
        var resources = tables.Select(Transform.TableResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{tableId:int}")]
    [SwaggerOperation("Get Table by Id", "Get a table by its unique identifier.", OperationId = "GetTableById")]
    [SwaggerResponse(200, "The table was found and returned.", typeof(TableResource))]
    [SwaggerResponse(404, "The table was not found.")]
    public async Task<IActionResult> GetTableById(int tableId, CancellationToken cancellationToken)
    {
        var getTableByIdQuery = new GetTableByIdQuery(tableId);
        var table = await tableQueryService.Handle(getTableByIdQuery, cancellationToken);

        if (table == null)  return NotFound();
        return Ok(Transform.TableResourceFromEntityAssembler.ToResourceFromEntity(table));
    }

    [HttpPut("{tableId:int}")]
    [SwaggerOperation("Update Table", "Updates an existing table.", OperationId = "UpdateTable")]
    [SwaggerResponse(200, "The table was updated successfully.", typeof(TableResource))]
    [SwaggerResponse(400, "Invalid request.")]
    public async Task<IActionResult> UpdateTable(int tableId, [FromBody] UpdateTableResource resource, CancellationToken cancellationToken)
    {
        var command = Transform.UpdateTableCommandFromResourceAssembler.ToCommandFromResource(tableId, resource);
        var result = await tableCommandService.Handle(command, cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        return Ok(Transform.TableResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }

    [HttpDelete("{tableId:int}")]
    [SwaggerOperation("Delete Table", "Deletes an existing table.", OperationId = "DeleteTable")]
    [SwaggerResponse(204, "The table was deleted successfully.")]
    [SwaggerResponse(400, "Invalid request.")]
    public async Task<IActionResult> DeleteTable(int tableId, CancellationToken cancellationToken)
    {
        var command = new DeleteTableCommand(tableId);
        var result = await tableCommandService.Handle(command, cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        return NoContent();
    }
}