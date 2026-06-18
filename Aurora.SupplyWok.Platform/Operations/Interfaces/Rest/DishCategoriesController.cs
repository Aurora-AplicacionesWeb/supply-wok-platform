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
[SwaggerTag("Available Dish Category Endpoints.")]
public class DishCategoriesController(IDishCategoryQueryService dishCategoryQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get All Dish Categories", "Gets all available dish categories.", OperationId = "GetAllDishCategories")]
    [SwaggerResponse(200, "Dish categories retrieved successfully.", typeof(IEnumerable<DishCategoryResource>))]
    public async Task<IActionResult> GetAllDishCategories(CancellationToken cancellationToken)
    {
        var query = new GetAllDishCategoriesQuery();
        var categories = await dishCategoryQueryService.Handle(query, cancellationToken);
        var resources = categories.Select(DishCategoryResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}
