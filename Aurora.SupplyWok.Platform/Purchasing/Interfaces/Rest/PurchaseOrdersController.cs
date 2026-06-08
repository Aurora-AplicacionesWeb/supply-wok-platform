using System.Net.Mime;
using Aurora.SupplyWok.Platform.Purchasing.Application.CommandServices;
using Aurora.SupplyWok.Platform.Purchasing.Application.QueryServices;
using Aurora.SupplyWok.Platform.Purchasing.Domain.Model;
using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Purchasing.Interfaces.Rest.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Aurora.SupplyWok.Platform.Purchasing.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Purchase Order Endpoints.")]
public class PurchaseOrdersController(
    IPurchaseOrderCommandService purchaseOrderCommandService,
    IPurchaseOrderQueryService purchaseOrderQueryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation("Create Purchase Order", "Creates a new purchase order.", OperationId = "CreatePurchaseOrder")]
    [SwaggerResponse(201, "The purchase order was created successfully.", typeof(PurchaseOrderResource))]
    [SwaggerResponse(400, "Invalid request.")]
    public async Task<IActionResult> CreatePurchaseOrder([FromBody] CreatePurchaseOrderResource resource, CancellationToken cancellationToken)
    {
        var command = Transform.CreatePurchaseOrderCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await purchaseOrderCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess) return ToFailureResponse(result.Error, result.Message);

        var purchaseOrderResource = Transform.PurchaseOrderResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(nameof(GetPurchaseOrderById), new { purchaseOrderId = purchaseOrderResource.Id }, purchaseOrderResource);
    }

    [HttpGet]
    [SwaggerOperation("Get All Purchase Orders", "Gets all purchase orders.", OperationId = "GetAllPurchaseOrders")]
    [SwaggerResponse(200, "Purchase orders retrieved successfully.", typeof(IEnumerable<PurchaseOrderResource>))]
    public async Task<IActionResult> GetAllPurchaseOrders(CancellationToken cancellationToken)
    {
        var query = new GetAllPurchaseOrdersQuery();
        var orders = await purchaseOrderQueryService.Handle(query, cancellationToken);
        var resources = orders.Select(Transform.PurchaseOrderResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{purchaseOrderId:int}")]
    [SwaggerOperation("Get Purchase Order by Id", "Gets a purchase order by its unique identifier.", OperationId = "GetPurchaseOrderById")]
    [SwaggerResponse(200, "The purchase order was found and returned.", typeof(PurchaseOrderResource))]
    [SwaggerResponse(404, "The purchase order was not found.")]
    public async Task<IActionResult> GetPurchaseOrderById(int purchaseOrderId, CancellationToken cancellationToken)
    {
        var query = new GetPurchaseOrderByIdQuery(purchaseOrderId);
        var order = await purchaseOrderQueryService.Handle(query, cancellationToken);

        if (order == null) return NotFound();
        return Ok(Transform.PurchaseOrderResourceFromEntityAssembler.ToResourceFromEntity(order));
    }

    [HttpPut("{purchaseOrderId:int}")]
    [SwaggerOperation("Update Purchase Order", "Updates an existing purchase order.", OperationId = "UpdatePurchaseOrder")]
    [SwaggerResponse(200, "The purchase order was updated successfully.", typeof(PurchaseOrderResource))]
    [SwaggerResponse(400, "Invalid request.")]
    [SwaggerResponse(404, "The purchase order was not found.")]
    public async Task<IActionResult> UpdatePurchaseOrder(int purchaseOrderId, [FromBody] UpdatePurchaseOrderResource resource, CancellationToken cancellationToken)
    {
        var command = Transform.UpdatePurchaseOrderCommandFromResourceAssembler.ToCommandFromResource(purchaseOrderId, resource);
        var result = await purchaseOrderCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess) return ToFailureResponse(result.Error, result.Message);
        return Ok(Transform.PurchaseOrderResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }

    [HttpPut("{purchaseOrderId:int}/status")]
    [SwaggerOperation("Update Purchase Order Status", "Updates the status of an existing purchase order.", OperationId = "UpdatePurchaseOrderStatus")]
    [SwaggerResponse(200, "The purchase order status was updated successfully.", typeof(PurchaseOrderResource))]
    [SwaggerResponse(400, "Invalid request.")]
    [SwaggerResponse(404, "The purchase order was not found.")]
    public async Task<IActionResult> UpdatePurchaseOrderStatus(int purchaseOrderId, [FromBody] UpdatePurchaseOrderStatusResource resource, CancellationToken cancellationToken)
    {
        var command = Transform.UpdatePurchaseOrderStatusCommandFromResourceAssembler.ToCommandFromResource(purchaseOrderId, resource);
        var result = await purchaseOrderCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess) return ToFailureResponse(result.Error, result.Message);
        return Ok(Transform.PurchaseOrderResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }

    [HttpDelete("{purchaseOrderId:int}")]
    [SwaggerOperation("Delete Purchase Order", "Deletes an existing purchase order.", OperationId = "DeletePurchaseOrder")]
    [SwaggerResponse(204, "The purchase order was deleted successfully.")]
    [SwaggerResponse(404, "The purchase order was not found.")]
    public async Task<IActionResult> DeletePurchaseOrder(int purchaseOrderId, CancellationToken cancellationToken)
    {
        var command = new DeletePurchaseOrderCommand(purchaseOrderId);
        var result = await purchaseOrderCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess) return ToFailureResponse(result.Error, result.Message);
        return NoContent();
    }

    private IActionResult ToFailureResponse(Enum? error, string message)
    {
        if (error is PurchaseOrdersError.PurchaseOrderNotFound or PurchaseOrdersError.SupplierNotFound)
            return NotFound(message);

        return BadRequest(message);
    }
}
