using Acme.Center.Platform.Resources.Errors;
using Acme.Center.Platform.Resources.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Acme.Center.Platform.Shared.Interfaces.Rest.ProblemDetails;

public class ProblemDetailsFactory(
    IStringLocalizer<ErrorMessages> errorLocalizer,
    IStringLocalizer<CommonMessages> commonLocalizer,
    Microsoft.AspNetCore.Mvc.Infrastructure.ProblemDetailsFactory aspNetCoreProblemDetailsFactory)
{
    public IActionResult CreateProblemDetails(
        ControllerBase controller,
        int statusCode,
        Enum? errorEnum,
        string detailMessage)
    {
        var title = errorEnum != null
            ? errorLocalizer[$"{errorEnum.GetType().Name}.{errorEnum}"]
            : commonLocalizer["GenericError"];

        var problemDetails = aspNetCoreProblemDetailsFactory.CreateProblemDetails(
            controller.HttpContext,
            statusCode,
            title,
            detail: detailMessage,
            instance: controller.HttpContext.Request.Path);

        return controller.StatusCode(statusCode, problemDetails);
    }

    public IActionResult CreateProblemDetails(
        ControllerBase controller,
        int statusCode,
        string titleKey,
        string detailKey,
        params object[] detailArgs)
    {
        var problemDetails = aspNetCoreProblemDetailsFactory.CreateProblemDetails(
            controller.HttpContext,
            statusCode,
            commonLocalizer[titleKey],
            detail: errorLocalizer[detailKey, detailArgs],
            instance: controller.HttpContext.Request.Path);

        return controller.StatusCode(statusCode, problemDetails);
    }
}
