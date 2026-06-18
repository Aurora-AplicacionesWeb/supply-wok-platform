using System.Net.Mime;
using Aurora.SupplyWok.Platform.Suppliers.Application.CommandServices;
using Aurora.SupplyWok.Platform.Suppliers.Application.QueryServices;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Model;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Suppliers.Interfaces.Rest.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Aurora.SupplyWok.Platform.Suppliers.Interfaces.Rest;

[ApiController]
[Route("api/v1/suppliers/{supplierId:int}/catalog-items")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available supplier catalog item endpoints.")]
public class CatalogItemsController(
    ICatalogItemCommandService catalogItemCommandService,
    ICatalogItemQueryService catalogItemQueryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation("Create Catalog Item", "Creates a new catalog item for a supplier.", OperationId = "CreateCatalogItem")]
    [SwaggerResponse(201, "The catalog item was created successfully.", typeof(CatalogItemResource))]
    [SwaggerResponse(400, "Invalid request.")]
    [SwaggerResponse(404, "The supplier was not found.")]
    public async Task<IActionResult> CreateCatalogItem(int supplierId, [FromBody] CreateCatalogItemResource resource, CancellationToken cancellationToken)
    {
        var command = Transform.CreateCatalogItemCommandFromResourceAssembler.ToCommandFromResource(supplierId, resource);
        var result = await catalogItemCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess) return ToFailureResponse(result.Error, result.Message);

        var catalogItemResource = Transform.CatalogItemResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(nameof(GetCatalogItemById), new { supplierId, catalogItemId = catalogItemResource.Id }, catalogItemResource);
    }

    [HttpGet]
    [SwaggerOperation("Get All Catalog Items By Supplier Id", "Gets all catalog items for a supplier.", OperationId = "GetAllCatalogItemsBySupplierId")]
    [SwaggerResponse(200, "Catalog items retrieved successfully.", typeof(IEnumerable<CatalogItemResource>))]
    public async Task<IActionResult> GetAllCatalogItemsBySupplierId(int supplierId, CancellationToken cancellationToken)
    {
        var query = new GetAllCatalogItemsBySupplierIdQuery(supplierId);
        var catalogItems = await catalogItemQueryService.Handle(query, cancellationToken);
        var resources = catalogItems.Select(Transform.CatalogItemResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{catalogItemId:int}")]
    [SwaggerOperation("Get Catalog Item By Id", "Gets a catalog item by supplier and item id.", OperationId = "GetCatalogItemById")]
    [SwaggerResponse(200, "The catalog item was found and returned.", typeof(CatalogItemResource))]
    [SwaggerResponse(404, "The catalog item was not found.")]
    public async Task<IActionResult> GetCatalogItemById(int supplierId, int catalogItemId, CancellationToken cancellationToken)
    {
        var query = new GetCatalogItemByIdQuery(supplierId, catalogItemId);
        var catalogItem = await catalogItemQueryService.Handle(query, cancellationToken);

        if (catalogItem is null) return NotFound();
        return Ok(Transform.CatalogItemResourceFromEntityAssembler.ToResourceFromEntity(catalogItem));
    }

    [HttpPut("{catalogItemId:int}")]
    [SwaggerOperation("Update Catalog Item", "Updates a supplier catalog item.", OperationId = "UpdateCatalogItem")]
    [SwaggerResponse(200, "The catalog item was updated successfully.", typeof(CatalogItemResource))]
    [SwaggerResponse(400, "Invalid request.")]
    [SwaggerResponse(404, "The supplier or catalog item was not found.")]
    public async Task<IActionResult> UpdateCatalogItem(int supplierId, int catalogItemId, [FromBody] UpdateCatalogItemResource resource, CancellationToken cancellationToken)
    {
        var command = Transform.UpdateCatalogItemCommandFromResourceAssembler.ToCommandFromResource(supplierId, catalogItemId, resource);
        var result = await catalogItemCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess) return ToFailureResponse(result.Error, result.Message);
        return Ok(Transform.CatalogItemResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }

    [HttpDelete("{catalogItemId:int}")]
    [SwaggerOperation("Delete Catalog Item", "Deletes a supplier catalog item.", OperationId = "DeleteCatalogItem")]
    [SwaggerResponse(204, "The catalog item was deleted successfully.")]
    [SwaggerResponse(404, "The supplier or catalog item was not found.")]
    public async Task<IActionResult> DeleteCatalogItem(int supplierId, int catalogItemId, CancellationToken cancellationToken)
    {
        var command = new DeleteCatalogItemCommand(supplierId, catalogItemId);
        var result = await catalogItemCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess) return ToFailureResponse(result.Error, result.Message);
        return NoContent();
    }

    private IActionResult ToFailureResponse(Enum? error, string message)
    {
        if (error is SuppliersError.SupplierNotFound or SuppliersError.CatalogItemNotFound)
            return NotFound(message);

        return BadRequest(message);
    }
}
