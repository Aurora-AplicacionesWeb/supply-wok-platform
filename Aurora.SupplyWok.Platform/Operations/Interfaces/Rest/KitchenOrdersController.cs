using System.Net.Mime;
using Aurora.SupplyWok.Platform.Operations.Application.CommandServices;
using Aurora.SupplyWok.Platform.Operations.Application.QueryServices;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Operations.Interfaces.Rest.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Aurora.SupplyWok.Platform.Operations.Interfaces.Rest;

[ApiController]
[Route("api/v1/kitchen-orders")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Kitchen Order Endpoints.")]
public class KitchenOrdersController(
    IKitchenOrderCommandService kitchenOrderCommandService,
    IKitchenOrderQueryService kitchenOrderQueryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation("Create Kitchen Order", "Creates a new kitchen order.", OperationId = "CreateKitchenOrder")]
    [SwaggerResponse(201, "The kitchen order was created successfully.", typeof(KitchenOrderResource))]
    [SwaggerResponse(400, "Invalid request.")]
    public async Task<IActionResult> CreateKitchenOrder([FromBody] CreateKitchenOrderResource resource, CancellationToken cancellationToken)
    {
        var command = Transform.CreateKitchenOrderCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await kitchenOrderCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        var kitchenOrderResource = Transform.KitchenOrderResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(nameof(GetKitchenOrderById), new { id = kitchenOrderResource.Id }, kitchenOrderResource);
    }

    [HttpGet]
    [SwaggerOperation("Get All Kitchen Orders", "Gets all available kitchen orders.", OperationId = "GetAllKitchenOrders")]
    [SwaggerResponse(200, "Kitchen orders retrieved successfully.", typeof(IEnumerable<KitchenOrderResource>))]
    public async Task<IActionResult> GetAllKitchenOrders(CancellationToken cancellationToken)
    {
        var query = new GetAllKitchenOrdersQuery();
        var kitchenOrders = await kitchenOrderQueryService.Handle(query, cancellationToken);
        var resources = kitchenOrders.Select(Transform.KitchenOrderResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation("Get Kitchen Order by Id", "Get a kitchen order by its unique identifier.", OperationId = "GetKitchenOrderById")]
    [SwaggerResponse(200, "The kitchen order was found and returned.", typeof(KitchenOrderResource))]
    [SwaggerResponse(404, "The kitchen order was not found.")]
    public async Task<IActionResult> GetKitchenOrderById(int id, CancellationToken cancellationToken)
    {
        var getKitchenOrderByIdQuery = new GetKitchenOrderByIdQuery(id);
        var kitchenOrder = await kitchenOrderQueryService.Handle(getKitchenOrderByIdQuery, cancellationToken);

        if (kitchenOrder == null)
            return NotFound();
        return Ok(Transform.KitchenOrderResourceFromEntityAssembler.ToResourceFromEntity(kitchenOrder));
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation("Update Kitchen Order", "Updates an existing kitchen order.", OperationId = "UpdateKitchenOrder")]
    [SwaggerResponse(200, "The kitchen order was updated successfully.", typeof(KitchenOrderResource))]
    [SwaggerResponse(400, "Invalid request.")]
    public async Task<IActionResult> UpdateKitchenOrder(int id, [FromBody] CreateKitchenOrderResource resource, CancellationToken cancellationToken)
    {
        var command = new UpdateKitchenOrderCommand(id, resource.Number, resource.TableId, resource.TypeService, resource.Observations, resource.DateCreated);
        var result = await kitchenOrderCommandService.Handle(command, cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        return Ok(Transform.KitchenOrderResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation("Delete Kitchen Order", "Deletes an existing kitchen order.", OperationId = "DeleteKitchenOrder")]
    [SwaggerResponse(204, "The kitchen order was deleted successfully.")]
    [SwaggerResponse(400, "Invalid request.")]
    public async Task<IActionResult> DeleteKitchenOrder(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteKitchenOrderCommand(id);
        var result = await kitchenOrderCommandService.Handle(command, cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        return NoContent();
    }

    [HttpPut("{id:int}/status")]
    [SwaggerOperation("Update Kitchen Order Status", "Updates the status of a kitchen order.", OperationId = "UpdateKitchenOrderStatus")]
    [SwaggerResponse(200, "The kitchen order status was updated successfully.", typeof(KitchenOrderResource))]
    [SwaggerResponse(400, "Invalid request.")]
    public async Task<IActionResult> UpdateKitchenOrderStatus(int id, [FromBody] UpdateKitchenOrderStatusResource resource, CancellationToken cancellationToken)
    {
        var command = new UpdateKitchenOrderStatusCommand(id, resource.Status);
        var result = await kitchenOrderCommandService.Handle(command, cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        return Ok(Transform.KitchenOrderResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }

    [HttpPost("{id:int}/dishes")]
    [SwaggerOperation("Add Dish to Kitchen Order", "Adds a dish to a kitchen order.", OperationId = "AddDishToKitchenOrder")]
    [SwaggerResponse(200, "The dish was added to the kitchen order successfully.", typeof(KitchenOrderResource))]
    [SwaggerResponse(400, "Invalid request.")]
    public async Task<IActionResult> AddDishToKitchenOrder(int id, [FromBody] AddDishToKitchenOrderResource resource, CancellationToken cancellationToken)
    {
        var command = Transform.AddDishToKitchenOrderCommandFromResourceAssembler.ToCommandFromResource(resource, id);
        var result = await kitchenOrderCommandService.Handle(command, cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        return Ok(Transform.KitchenOrderResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }
}
