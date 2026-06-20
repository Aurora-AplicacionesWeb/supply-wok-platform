using System.Net.Mime;
using Aurora.SupplyWok.Platform.Analytics.Application.QueryServices;
using Aurora.SupplyWok.Platform.Analytics.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Analytics.Interfaces.Rest.Resources;
using Aurora.SupplyWok.Platform.Analytics.Interfaces.Rest.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Aurora.SupplyWok.Platform.Analytics.Interfaces.Rest.Controllers;

[ApiController]
[Route("api/v1/analytics/supplier")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Supplier Analytics Endpoints.")]
public class SupplierAnalyticsController(ISupplierAnalyticsQueryService queryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get Supplier Analytics", "Retrieves supplier report data.", OperationId = "GetSupplierAnalytics")]
    [SwaggerResponse(200, "Supplier analytics retrieved successfully.", typeof(IEnumerable<SupplierAnalyticsResource>))]
    public async Task<IActionResult> GetSupplierAnalytics(CancellationToken cancellationToken)
    {
        var query = new GetAllSupplierAnalyticsQuery();
        var result = await queryService.Handle(query, cancellationToken);
        var resources = result.Select(SupplierAnalyticsResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}
