using System.Net.Mime;
using Aurora.SupplyWok.Platform.Profiles.Application.CommandServices;
using Aurora.SupplyWok.Platform.Profiles.Application.QueryServices;
using Aurora.SupplyWok.Platform.Profiles.Domain.Model;
using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Profiles.Interfaces.Rest.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Aurora.SupplyWok.Platform.Profiles.Interfaces.Rest;

/// <summary>
///     Rest controller for supplier profiles
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Supplier Profile Endpoints.")]
public class SupplierProfilesController(
    ISupplierProfileCommandService supplierProfileCommandService,
    ISupplierProfileQueryService supplierProfileQueryService) : ControllerBase
{
    /// <summary>
    ///     Create a new supplier profile
    /// </summary>
    /// <param name="resource">The data of the supplier profile to create.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created supplier profile.</returns>
    [HttpPost]
    [SwaggerOperation("Create Supplier Profile", "Creates a new supplier profile.", OperationId = "CreateSupplierProfile")]
    [SwaggerResponse(201, "The supplier profile was created successfully.", typeof(SupplierProfileResource))]
    [SwaggerResponse(400, "Invalid request.")]
    public async Task<IActionResult> CreateSupplierProfile([FromBody] CreateSupplierProfileResource resource, CancellationToken cancellationToken)
    {
        var command = Transform.CreateSupplierProfileCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await supplierProfileCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess) return ToFailureResponse(result.Error, result.Message);

        var supplierProfileResource = Transform.SupplierProfileResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(nameof(GetSupplierProfileById), new { supplierProfileId = supplierProfileResource.Id }, supplierProfileResource);
    }

    /// <summary>
    ///     Get all supplier profiles
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The list of all supplier profiles.</returns>
    [HttpGet]
    [SwaggerOperation("Get All Supplier Profiles", "Gets all supplier profiles.", OperationId = "GetAllSupplierProfiles")]
    [SwaggerResponse(200, "Supplier profiles retrieved successfully.", typeof(IEnumerable<SupplierProfileResource>))]
    public async Task<IActionResult> GetAllSupplierProfiles(CancellationToken cancellationToken)
    {
        var query = new GetAllSupplierProfilesQuery();
        var supplierProfiles = await supplierProfileQueryService.Handle(query, cancellationToken);
        var resources = supplierProfiles.Select(Transform.SupplierProfileResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /// <summary>
    ///     Get a supplier profile by its id
    /// </summary>
    /// <param name="supplierProfileId">The id of the supplier profile to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The supplier profile if found.</returns>
    [HttpGet("{supplierProfileId:int}")]
    [SwaggerOperation("Get Supplier Profile by Id", "Gets a supplier profile by its unique identifier.", OperationId = "GetSupplierProfileById")]
    [SwaggerResponse(200, "The supplier profile was found and returned.", typeof(SupplierProfileResource))]
    [SwaggerResponse(404, "The supplier profile was not found.")]
    public async Task<IActionResult> GetSupplierProfileById(int supplierProfileId, CancellationToken cancellationToken)
    {
        var query = new GetSupplierProfileByIdQuery(supplierProfileId);
        var supplierProfile = await supplierProfileQueryService.Handle(query, cancellationToken);

        if (supplierProfile is null) return NotFound();
        return Ok(Transform.SupplierProfileResourceFromEntityAssembler.ToResourceFromEntity(supplierProfile));
    }

    /// <summary>
    ///     Get a supplier profile by its linked Iam user id
    /// </summary>
    /// <param name="userId">The Iam user id to search for.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The supplier profile if found.</returns>
    
    //[HttpGet("by-user/{userId:int}")]
    [HttpGet("~/api/v1/users/{userId:int}/supplier-profiles")]
        
    [SwaggerOperation("Get Supplier Profile by User Id", "Gets a supplier profile by its linked Iam user id.", OperationId = "GetSupplierProfileByUserId")]
    [SwaggerResponse(200, "The supplier profile was found and returned.", typeof(SupplierProfileResource))]
    [SwaggerResponse(404, "The supplier profile was not found.")]
    public async Task<IActionResult> GetSupplierProfileByUserId(int userId, CancellationToken cancellationToken)
    {
        var query = new GetSupplierProfileByUserIdQuery(userId);
        var supplierProfile = await supplierProfileQueryService.Handle(query, cancellationToken);

        if (supplierProfile is null) return NotFound();
        return Ok(Transform.SupplierProfileResourceFromEntityAssembler.ToResourceFromEntity(supplierProfile));
    }

    /// <summary>
    ///     Translate a <see cref="ProfilesError" /> into the corresponding HTTP failure response
    /// </summary>
    /// <param name="error">The error returned by the command service, if any.</param>
    /// <param name="message">The error message.</param>
    /// <returns>The corresponding <see cref="IActionResult" />.</returns>
    private IActionResult ToFailureResponse(Enum? error, string message)
    {
        if (error is ProfilesError.SupplierProfileNotFound)
            return NotFound(message);

        if (error is ProfilesError.InvalidData or ProfilesError.DuplicateEmail)
            return BadRequest(message);

        return StatusCode(500, message);
    }
}
