using System.Net.Mime;
using Aurora.SupplyWok.Platform.Operations.Application.CommandServices;
using Aurora.SupplyWok.Platform.Operations.Application.QueryServices;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Operations.Interfaces.Rest.Resources;
using Aurora.SupplyWok.Platform.Operations.Interfaces.Rest.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Aurora.SupplyWok.Platform.Operations.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Dish Endpoints.")]
public class DishesController(
    IDishCommandService dishCommandService,
    IDishQueryService dishQueryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation("Create Dish", "Creates a new dish.", OperationId = "CreateDish")]
    [SwaggerResponse(201, "The dish was created successfully.", typeof(DishResource))]
    [SwaggerResponse(400, "Invalid request.")]
    public async Task<IActionResult> CreateDish([FromBody] CreateDishResource resource, CancellationToken cancellationToken)
    {
        var command = CreateDishCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await dishCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        var dishResource = DishResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(nameof(GetDishById), new { dishId = dishResource.Id }, dishResource);
    }

    [HttpGet]
    [SwaggerOperation("Get All Dishes", "Gets all available dishes.", OperationId = "GetAllDishes")]
    [SwaggerResponse(200, "Dishes retrieved successfully.", typeof(IEnumerable<DishResource>))]
    public async Task<IActionResult> GetAllDishes(CancellationToken cancellationToken)
    {
        var query = new GetAllDishesQuery();
        var dishes = await dishQueryService.Handle(query, cancellationToken);
        var resources = dishes.Select(DishResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{dishId:int}")]
    [SwaggerOperation("Get Dish by Id", "Gets a dish by its unique identifier.", OperationId = "GetDishById")]
    [SwaggerResponse(200, "The dish was found and returned.", typeof(DishResource))]
    [SwaggerResponse(404, "The dish was not found.")]
    public async Task<IActionResult> GetDishById(int dishId, CancellationToken cancellationToken)
    {
        var query = new GetDishByIdQuery(dishId);
        var dish = await dishQueryService.Handle(query, cancellationToken);

        if (dish == null)
            return NotFound();

        return Ok(DishResourceFromEntityAssembler.ToResourceFromEntity(dish));
    }
}
