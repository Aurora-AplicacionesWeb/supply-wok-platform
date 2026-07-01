using System.Net.Mime;
using Aurora.SupplyWok.Platform.Spm.Application.QueryServices;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Spm.Interfaces.Rest.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Aurora.SupplyWok.Platform.Spm.Interfaces.Rest;

/// <summary>
///     REST controller for supplier client endpoints.
/// </summary>
[ApiController]
[Route("api/v1/suppliers/{supplierId:int}/clients")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available client endpoints for the supplier workspace.")]
public class ClientsController(IClientQueryService clientQueryService) : ControllerBase
{
    /// <summary>
    ///     Gets all clients linked to the given supplier.
    /// </summary>
    /// <param name="supplierId">The supplier identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The list of client resources.</returns>
    [HttpGet]
    [SwaggerOperation("Get All Clients By Supplier Id", "Gets all clients linked to the given supplier.", OperationId = "GetAllClientsBySupplierId")]
    [SwaggerResponse(200, "Clients retrieved successfully.", typeof(IEnumerable<ClientResource>))]
    public async Task<IActionResult> GetAllClientsBySupplierId(int supplierId, CancellationToken cancellationToken)
    {
        var query = new GetAllClientsBySupplierIdQuery(supplierId);
        var clients = await clientQueryService.Handle(query, cancellationToken);
        var resources = clients.Select(Transform.ClientResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}
