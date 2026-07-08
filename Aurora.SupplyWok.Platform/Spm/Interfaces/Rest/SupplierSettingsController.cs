using System.Net.Mime;
using Aurora.SupplyWok.Platform.Spm.Application.CommandServices;
using Aurora.SupplyWok.Platform.Spm.Application.QueryServices;
using Aurora.SupplyWok.Platform.Spm.Domain.Model;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Spm.Interfaces.Rest.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Aurora.SupplyWok.Platform.Spm.Interfaces.Rest;

[ApiController]
[Route("api/v1/suppliers/{supplierProfileId:int}/settings")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available supplier settings endpoints.")]
public class SupplierSettingsController(
    ISupplierSettingsCommandService supplierSettingsCommandService,
    ISupplierSettingsQueryService supplierSettingsQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get Supplier Settings", "Gets the settings for a supplier.", OperationId = "GetSupplierSettings")]
    [SwaggerResponse(200, "The supplier settings were found and returned.", typeof(SupplierSettingsResource))]
    [SwaggerResponse(404, "The supplier settings were not found.")]
    public async Task<IActionResult> GetSupplierSettings(int supplierProfileId, CancellationToken cancellationToken)
    {
        var query = new GetSupplierSettingsBySupplierProfileIdQuery(supplierProfileId);
        var settings = await supplierSettingsQueryService.Handle(query, cancellationToken);

        if (settings is null) return NotFound();
        return Ok(Transform.SupplierSettingsResourceFromEntityAssembler.ToResourceFromEntity(settings));
    }

    [HttpPut]
    [SwaggerOperation("Update Supplier Settings", "Updates the settings for a supplier.", OperationId = "UpdateSupplierSettings")]
    [SwaggerResponse(200, "The supplier settings were updated successfully.", typeof(SupplierSettingsResource))]
    [SwaggerResponse(400, "Invalid request.")]
    [SwaggerResponse(404, "The supplier settings were not found.")]
    public async Task<IActionResult> UpdateSupplierSettings(int supplierProfileId, [FromBody] UpdateSupplierSettingsResource resource, CancellationToken cancellationToken)
    {
        var command = Transform.UpdateSupplierSettingsCommandFromResourceAssembler.ToCommandFromResource(supplierProfileId, resource);
        var result = await supplierSettingsCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess) return ToFailureResponse(result.Error, result.Message);
        return Ok(Transform.SupplierSettingsResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }

    private IActionResult ToFailureResponse(Enum? error, string message)
    {
        if (error is SuppliersError.SupplierSettingsNotFound or SuppliersError.SupplierNotFound)
            return NotFound(message);

        return BadRequest(message);
    }
}
