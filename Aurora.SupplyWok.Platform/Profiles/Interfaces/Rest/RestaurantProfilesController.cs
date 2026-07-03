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
///     Rest controller for restaurant profiles
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Restaurant Profile Endpoints.")]
public class RestaurantProfilesController(
    IRestaurantProfileCommandService restaurantProfileCommandService,
    IRestaurantProfileQueryService restaurantProfileQueryService) : ControllerBase
{
    /// <summary>
    ///     Create a new restaurant profile
    /// </summary>
    /// <param name="resource">The data of the restaurant profile to create.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created restaurant profile.</returns>
    [HttpPost]
    [SwaggerOperation("Create Restaurant Profile", "Creates a new restaurant profile.", OperationId = "CreateRestaurantProfile")]
    [SwaggerResponse(201, "The restaurant profile was created successfully.", typeof(RestaurantProfileResource))]
    [SwaggerResponse(400, "Invalid request.")]
    public async Task<IActionResult> CreateRestaurantProfile([FromBody] CreateRestaurantProfileResource resource, CancellationToken cancellationToken)
    {
        var command = Transform.CreateRestaurantProfileCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await restaurantProfileCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess) return ToFailureResponse(result.Error, result.Message);

        var restaurantProfileResource = Transform.RestaurantProfileResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(nameof(GetRestaurantProfileById), new { restaurantProfileId = restaurantProfileResource.Id }, restaurantProfileResource);
    }

    /// <summary>
    ///     Get all restaurant profiles
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The list of all restaurant profiles.</returns>
    [HttpGet]
    [SwaggerOperation("Get All Restaurant Profiles", "Gets all restaurant profiles.", OperationId = "GetAllRestaurantProfiles")]
    [SwaggerResponse(200, "Restaurant profiles retrieved successfully.", typeof(IEnumerable<RestaurantProfileResource>))]
    public async Task<IActionResult> GetAllRestaurantProfiles(CancellationToken cancellationToken)
    {
        var query = new GetAllRestaurantProfilesQuery();
        var restaurantProfiles = await restaurantProfileQueryService.Handle(query, cancellationToken);
        var resources = restaurantProfiles.Select(Transform.RestaurantProfileResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /// <summary>
    ///     Get a restaurant profile by its id
    /// </summary>
    /// <param name="restaurantProfileId">The id of the restaurant profile to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The restaurant profile if found.</returns>
    [HttpGet("{restaurantProfileId:int}")]
    [SwaggerOperation("Get Restaurant Profile by Id", "Gets a restaurant profile by its unique identifier.", OperationId = "GetRestaurantProfileById")]
    [SwaggerResponse(200, "The restaurant profile was found and returned.", typeof(RestaurantProfileResource))]
    [SwaggerResponse(404, "The restaurant profile was not found.")]
    public async Task<IActionResult> GetRestaurantProfileById(int restaurantProfileId, CancellationToken cancellationToken)
    {
        var query = new GetRestaurantProfileByIdQuery(restaurantProfileId);
        var restaurantProfile = await restaurantProfileQueryService.Handle(query, cancellationToken);

        if (restaurantProfile is null) return NotFound();
        return Ok(Transform.RestaurantProfileResourceFromEntityAssembler.ToResourceFromEntity(restaurantProfile));
    }

    /// <summary>
    ///     Get a restaurant profile by its linked Iam user id
    /// </summary>
    /// <param name="userId">The Iam user id to search for.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The restaurant profile if found.</returns>
    
    //[HttpGet("by-user/{userId:int}")]
    [HttpGet("~/api/v1/users/{userId:int}/restaurant-profiles")]
    [SwaggerOperation("Get Restaurant Profile by User Id", "Gets a restaurant profile by its linked Iam user id.", OperationId = "GetRestaurantProfileByUserId")]
    [SwaggerResponse(200, "The restaurant profile was found and returned.", typeof(RestaurantProfileResource))]
    [SwaggerResponse(404, "The restaurant profile was not found.")]
    public async Task<IActionResult> GetRestaurantProfileByUserId(int userId, CancellationToken cancellationToken)
    {
        var query = new GetRestaurantProfileByUserIdQuery(userId);
        var restaurantProfile = await restaurantProfileQueryService.Handle(query, cancellationToken);

        if (restaurantProfile is null) return NotFound();
        return Ok(Transform.RestaurantProfileResourceFromEntityAssembler.ToResourceFromEntity(restaurantProfile));
    }

    /// <summary>
    ///     Translate a <see cref="ProfilesError" /> into the corresponding HTTP failure response
    /// </summary>
    /// <param name="error">The error returned by the command service, if any.</param>
    /// <param name="message">The error message.</param>
    /// <returns>The corresponding <see cref="IActionResult" />.</returns>
    private IActionResult ToFailureResponse(Enum? error, string message)
    {
        if (error is ProfilesError.RestaurantProfileNotFound)
            return NotFound(message);

        if (error is ProfilesError.InvalidData or ProfilesError.DuplicateEmail)
            return BadRequest(message);

        return StatusCode(500, message);
    }
}
