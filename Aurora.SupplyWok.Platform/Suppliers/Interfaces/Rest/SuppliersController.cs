using System.Net.Mime;
using Aurora.SupplyWok.Platform.Suppliers.Application.QueryServices;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Suppliers.Interfaces.Rest.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Aurora.SupplyWok.Platform.Suppliers.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Supplier Endpoints.")]
public class SuppliersController(ISupplierQueryService supplierQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get All Suppliers", "Gets all suppliers.", OperationId = "GetAllSuppliers")]
    [SwaggerResponse(200, "Suppliers retrieved successfully.", typeof(IEnumerable<SupplierResource>))]
    public async Task<IActionResult> GetAllSuppliers(CancellationToken cancellationToken)
    {
        var query = new GetAllSuppliersQuery();
        var suppliers = await supplierQueryService.Handle(query, cancellationToken);
        var resources = suppliers.Select(Transform.SupplierResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}
