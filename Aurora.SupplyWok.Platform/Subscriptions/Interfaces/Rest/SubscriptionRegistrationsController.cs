using System.Net.Mime;
using Aurora.SupplyWok.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Aurora.SupplyWok.Platform.Subscriptions.Application.CommandServices;
using Aurora.SupplyWok.Platform.Subscriptions.Application.QueryServices;
using Aurora.SupplyWok.Platform.Subscriptions.Domain.Model;
using Aurora.SupplyWok.Platform.Subscriptions.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Subscriptions.Interfaces.Rest.Resources;
using Aurora.SupplyWok.Platform.Subscriptions.Interfaces.Rest.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Aurora.SupplyWok.Platform.Subscriptions.Interfaces.Rest;

[ApiController]
[Route("api/v1/subscriptions/registrations")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available subscription registration endpoints.")]
public class SubscriptionRegistrationsController(
    ISubscriptionRegistrationCommandService subscriptionRegistrationCommandService,
    ISubscriptionRegistrationQueryService subscriptionRegistrationQueryService) : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Start subscription registration",
        Description = "Creates a pending registration and returns the Stripe checkout url.",
        OperationId = "StartSubscriptionRegistration")]
    public async Task<IActionResult> StartRegistration(
        [FromBody] StartSubscriptionRegistrationResource resource,
        CancellationToken cancellationToken)
    {
        var command = StartSubscriptionRegistrationCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await subscriptionRegistrationCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess)
            return ToFailureResponse(result.Error, result.Message);

        return CreatedAtAction(
            nameof(GetRegistrationStatus),
            new { publicId = result.Value!.registration.PublicId },
            SubscriptionRegistrationResourceFromEntityAssembler.ToResourceFromEntity(
                result.Value.registration,
                result.Value.checkoutUrl));
    }

    [HttpGet("{publicId:guid}")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Get registration status",
        Description = "Gets the current provisioning status for a pending registration.",
        OperationId = "GetSubscriptionRegistrationStatus")]
    public async Task<IActionResult> GetRegistrationStatus(Guid publicId, CancellationToken cancellationToken)
    {
        var pendingRegistration = await subscriptionRegistrationQueryService.Handle(
            new GetRegistrationStatusQuery(publicId), cancellationToken);

        if (pendingRegistration is null)
            return NotFound(new { message = "Registration not found." });

        return Ok(SubscriptionRegistrationResourceFromEntityAssembler.ToStatusResourceFromEntity(pendingRegistration));
    }

    private IActionResult ToFailureResponse(Enum? error, string message)
    {
        return error switch
        {
            SubscriptionsError.InvalidPlan or SubscriptionsError.InvalidRole or SubscriptionsError.InvalidData =>
                BadRequest(new { message }),
            SubscriptionsError.DuplicateEmail => Conflict(new { message }),
            SubscriptionsError.RegistrationNotFound => NotFound(new { message }),
            _ => StatusCode(StatusCodes.Status500InternalServerError, new { message })
        };
    }
}
