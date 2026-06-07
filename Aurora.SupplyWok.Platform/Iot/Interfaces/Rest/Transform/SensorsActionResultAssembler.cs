using Aurora.SupplyWok.Platform.Iot.Domain.Model;
using Aurora.SupplyWok.Platform.Iot.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Aurora.SupplyWok.Platform.Shared.Resources.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Aurora.SupplyWok.Platform.Iot.Interfaces.Rest.Transform;

public static class SensorsActionResultAssembler
{
    private static int ToStatusCodeFromSensorsError(SensorsError error)
    {
        return error switch
        {
            SensorsError.SensorNotFound => StatusCodes.Status404NotFound,
            SensorsError.InternalServerError => StatusCodes.Status500InternalServerError,
            SensorsError.DatabaseError => StatusCodes.Status500InternalServerError,
            SensorsError.OperationCancelled => StatusCodes.Status400BadRequest,
            SensorsError.None => StatusCodes.Status200OK,
            _ => StatusCodes.Status400BadRequest
        };
    }

    public static IActionResult ToActionResultFromGetProfileByIdResult(
        ControllerBase controller,
        Sensor? sensor,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Sensor, IActionResult> successAction)
    {
        if (sensor is null)
            return problemDetailsFactory.CreateProblemDetails(
                controller,
                ToStatusCodeFromSensorsError(SensorsError.SensorNotFound),
                SensorsError.SensorNotFound,
                errorLocalizer[nameof(SensorsError.SensorNotFound)]
                );
        return successAction(sensor);
    }
}