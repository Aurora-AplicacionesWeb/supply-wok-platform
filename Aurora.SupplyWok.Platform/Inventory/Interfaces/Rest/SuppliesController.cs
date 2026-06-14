using System.Net.Mime;
using Aurora.SupplyWok.Platform.Inventory.Application.CommandServices;
using Aurora.SupplyWok.Platform.Inventory.Application.QueryServices;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Inventory.Interfaces.Rest.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Aurora.SupplyWok.Platform.Inventory.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Supply Endpoints.")]
public class SuppliesController(
    ISupplyCommandService supplyCommandService,
    ISupplyQueryServices supplyQueryServices) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation("Create Supply", "Creates a new supply item.", OperationId = "CreateSupply")]
    [SwaggerResponse(201, "The supply was created successfully.", typeof(SupplyResource))]
    [SwaggerResponse(400, "Invalid request.")]
    public async Task<IActionResult> CreateSupply([FromBody] CreateSupplyResource resource, CancellationToken cancellationToken)
    {
        var command = Transform.CreateSupplyCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await supplyCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess) return ToFailureResponse(result.Error, result.Message);

        var supplyResource = Transform.SupplyResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(nameof(GetSupplyById), new { supplyId = supplyResource.Id }, supplyResource);
    }

    [HttpGet]
    [SwaggerOperation("Get All Supplies", "Gets all supply items.", OperationId = "GetAllSupplies")]
    [SwaggerResponse(200, "Supplies retrieved successfully.", typeof(IEnumerable<SupplyResource>))]
    public async Task<IActionResult> GetAllSupplies(CancellationToken cancellationToken)
    {
        var query = new GetAllSuppliesQuery();
        var supplies = await supplyQueryServices.Handle(query, cancellationToken);
        var resources = supplies.Select(Transform.SupplyResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("total-stock")]
    [SwaggerOperation("Get Total Supply Stock", "Gets the total current stock across all supplies.", OperationId = "GetTotalSupplyStock")]
    [SwaggerResponse(200, "Total stock retrieved successfully.")]
    public async Task<IActionResult> GetTotalSupplyStock(CancellationToken cancellationToken)
    {
        var total = await supplyQueryServices.Handle(new GetTotalSupplyStockQuery(), cancellationToken);
        return Ok(new { totalCurrentStock = total });
    }

    [HttpGet("{supplyId:int}")]
    [SwaggerOperation("Get Supply by Id", "Gets a supply item by its unique identifier.", OperationId = "GetSupplyById")]
    [SwaggerResponse(200, "The supply was found and returned.", typeof(SupplyResource))]
    [SwaggerResponse(404, "The supply was not found.")]
    public async Task<IActionResult> GetSupplyById(int supplyId, CancellationToken cancellationToken)
    {
        var query = new GetSupplyByIdQuery(supplyId);
        var supply = await supplyQueryServices.Handle(query, cancellationToken);

        if (supply is null) return NotFound();
        return Ok(Transform.SupplyResourceFromEntityAssembler.ToResourceFromEntity(supply));
    }

    [HttpPut("{supplyId:int}")]
    [SwaggerOperation("Update Supply", "Updates an existing supply item.", OperationId = "UpdateSupply")]
    [SwaggerResponse(200, "The supply was updated successfully.", typeof(SupplyResource))]
    [SwaggerResponse(400, "Invalid request.")]
    [SwaggerResponse(404, "The supply was not found.")]
    public async Task<IActionResult> UpdateSupply(int supplyId, [FromBody] UpdateSupplyResource resource, CancellationToken cancellationToken)
    {
        var command = Transform.UpdateSupplyCommandFromResourceAssembler.ToCommandFromResource(supplyId, resource);
        var result = await supplyCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess) return ToFailureResponse(result.Error, result.Message);
        return Ok(Transform.SupplyResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }

    [HttpDelete("{supplyId:int}")]
    [SwaggerOperation("Delete Supply", "Deletes an existing supply item.", OperationId = "DeleteSupply")]
    [SwaggerResponse(204, "The supply was deleted successfully.")]
    [SwaggerResponse(404, "The supply was not found.")]
    public async Task<IActionResult> DeleteSupply(int supplyId, CancellationToken cancellationToken)
    {
        var command = new DeleteSupplyCommand(supplyId);
        var result = await supplyCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess) return ToFailureResponse(result.Error, result.Message);
        return NoContent();
    }

    private IActionResult ToFailureResponse(Enum? error, string message)
    {
        if (error is InventoryError.SupplyNotFound)
            return NotFound(message);

        return BadRequest(message);
    }
}
