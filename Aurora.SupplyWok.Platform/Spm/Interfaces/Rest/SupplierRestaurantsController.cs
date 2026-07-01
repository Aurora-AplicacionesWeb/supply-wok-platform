using System.Net.Mime;
using Aurora.SupplyWok.Platform.Profiles.Interfaces.Acl;
using Aurora.SupplyWok.Platform.Spm.Application.QueryServices;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Spm.Interfaces.Rest.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Aurora.SupplyWok.Platform.Spm.Interfaces.Rest;

[ApiController]
[Route("api/v1/suppliers/{supplierId:int}/restaurants")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available restaurant relationship endpoints for a supplier.")]
public class SupplierRestaurantsController(
    ISupplierRestaurantQueryService supplierRestaurantQueryService,
    IProfilesContextFacade profilesContextFacade) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get Restaurants By Supplier Id", "Gets restaurants linked to the given supplier profile.", OperationId = "GetRestaurantsBySupplierId")]
    [SwaggerResponse(200, "Restaurants retrieved successfully.", typeof(IEnumerable<RestaurantResource>))]
    [SwaggerResponse(404, "The supplier profile was not found.")]
    public async Task<IActionResult> GetRestaurantsBySupplierId(int supplierId, CancellationToken cancellationToken)
    {
        var supplierProfile = await profilesContextFacade.GetSupplierProfileById(supplierId, cancellationToken);
        if (supplierProfile is null) return NotFound();

        var links = (await supplierRestaurantQueryService.Handle(new GetRestaurantsBySupplierIdQuery(supplierId),
            cancellationToken)).ToList();
        var restaurantProfiles = await profilesContextFacade.GetRestaurantProfilesByIds(
            links.Select(link => link.RestaurantProfileId),
            cancellationToken);
        var restaurantProfilesById = restaurantProfiles.ToDictionary(profile => profile.Id);

        var resources = links
            .Where(link => restaurantProfilesById.ContainsKey(link.RestaurantProfileId))
            .Select(link =>
            {
                var profile = restaurantProfilesById[link.RestaurantProfileId];
                return new RestaurantResource(
                    profile.Id,
                    profile.BusinessName,
                    profile.District,
                    link.Status);
            });

        return Ok(resources);
    }
}
