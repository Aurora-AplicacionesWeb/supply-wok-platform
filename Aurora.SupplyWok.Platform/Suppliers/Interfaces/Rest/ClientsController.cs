using System.Net.Mime;
using Aurora.SupplyWok.Platform.Suppliers.Application.QueryServices;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Suppliers.Interfaces.Rest.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Aurora.SupplyWok.Platform.Suppliers.Interfaces.Rest;

/// <summary>
///     REST controller for supplier client endpoints.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available client endpoints for the supplier workspace.")]
public class ClientsController(IClientQueryService clientQueryService) : ControllerBase
{
    /// <summary>
    ///     Gets all clients visible in the supplier workspace.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The list of client resources.</returns>
    [HttpGet]
    [SwaggerOperation("Get All Clients", "Gets all clients visible in the supplier workspace.", OperationId = "GetAllClients")]
    [SwaggerResponse(200, "Clients retrieved successfully.", typeof(IEnumerable<ClientResource>))]
    public async Task<IActionResult> GetAllClients(CancellationToken cancellationToken)
    {
        var query = new GetAllClientsQuery();
        var clients = await clientQueryService.Handle(query, cancellationToken);
        var resources = clients.Select(Transform.ClientResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}
