using System.Net.Mime;
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
public class DishesController(IDishQueryService dishQueryService) : ControllerBase
{
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
}
