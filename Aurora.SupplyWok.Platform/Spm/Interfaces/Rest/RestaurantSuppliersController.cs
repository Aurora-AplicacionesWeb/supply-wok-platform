using System.Net.Mime;
using Aurora.SupplyWok.Platform.Profiles.Interfaces.Acl;
using Aurora.SupplyWok.Platform.Spm.Application.QueryServices;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Spm.Interfaces.Rest.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Aurora.SupplyWok.Platform.Spm.Interfaces.Rest;

[ApiController]
[Route("api/v1/restaurants/{restaurantId:int}/suppliers")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available supplier relationship endpoints for a restaurant.")]
public class RestaurantSuppliersController(
    ISupplierRestaurantQueryService supplierRestaurantQueryService,
    IProfilesContextFacade profilesContextFacade) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get Suppliers By Restaurant Id", "Gets suppliers linked to the given restaurant profile.", OperationId = "GetSuppliersByRestaurantId")]
    [SwaggerResponse(200, "Suppliers retrieved successfully.", typeof(IEnumerable<SupplierResource>))]
    [SwaggerResponse(404, "The restaurant profile was not found.")]
    public async Task<IActionResult> GetSuppliersByRestaurantId(int restaurantId, CancellationToken cancellationToken)
    {
        var restaurantProfile = await profilesContextFacade.GetRestaurantProfileById(restaurantId, cancellationToken);
        if (restaurantProfile is null) return NotFound();

        var links = (await supplierRestaurantQueryService.Handle(new GetSuppliersByRestaurantIdQuery(restaurantId),
            cancellationToken)).ToList();
        var supplierProfiles = await profilesContextFacade.GetSupplierProfilesByIds(
            links.Select(link => link.SupplierProfileId),
            cancellationToken);
        var supplierProfilesById = supplierProfiles.ToDictionary(profile => profile.Id);

        var resources = links
            .Where(link => supplierProfilesById.ContainsKey(link.SupplierProfileId))
            .Select(link =>
            {
                var profile = supplierProfilesById[link.SupplierProfileId];
                return new SupplierResource(
                    profile.Id,
                    profile.BusinessName,
                    profile.ContactName,
                    profile.Email,
                    profile.Phone,
                    profile.Category,
                    link.LinkedDate,
                    link.Sla,
                    link.ResponseTime);
            });

        return Ok(resources);
    }
}
