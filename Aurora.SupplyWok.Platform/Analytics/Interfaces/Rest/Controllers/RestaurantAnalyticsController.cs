using System.Net.Mime;
using Aurora.SupplyWok.Platform.Analytics.Application.QueryServices;
using Aurora.SupplyWok.Platform.Analytics.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Analytics.Interfaces.Rest.Resources;
using Aurora.SupplyWok.Platform.Analytics.Interfaces.Rest.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Aurora.SupplyWok.Platform.Analytics.Interfaces.Rest.Controllers;

[ApiController]
[Route("api/v1/analytics/restaurant")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Restaurant Analytics Endpoints.")]
public class RestaurantAnalyticsController(IRestaurantAnalyticsQueryService queryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get Restaurant Analytics", "Retrieves restaurant report data.", OperationId = "GetRestaurantAnalytics")]
    [SwaggerResponse(200, "Restaurant analytics retrieved successfully.", typeof(RestaurantReportsResponse))]
    public async Task<IActionResult> GetRestaurantAnalytics(CancellationToken cancellationToken)
    {
        var query = new GetAllRestaurantAnalyticsQuery();
        var result = await queryService.Handle(query, cancellationToken);
        var entity = result.FirstOrDefault();
        if (entity == null)
            return NotFound("No restaurant analytics data found.");

        var resource = RestaurantAnalyticsResourceFromEntityAssembler.ToResourceFromEntity(entity);
        var response = new RestaurantReportsResponse(resource);
        return Ok(response);
    }
}
