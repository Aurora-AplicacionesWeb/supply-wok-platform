using System.Net.Mime;
using System.Text;
using Aurora.SupplyWok.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Aurora.SupplyWok.Platform.Subscriptions.Application.CommandServices;
using Aurora.SupplyWok.Platform.Subscriptions.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Aurora.SupplyWok.Platform.Subscriptions.Interfaces.Rest;

[ApiController]
[Route("api/v1/subscriptions/webhooks/stripe")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Stripe webhook endpoint for subscriptions.")]
public class StripeWebhooksController(ISubscriptionWebhookService subscriptionWebhookService) : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Process Stripe webhook",
        Description = "Processes subscription related Stripe events.",
        OperationId = "ProcessStripeWebhook")]
    public async Task<IActionResult> ProcessStripeWebhook(CancellationToken cancellationToken)
    {
        var signatureHeader = Request.Headers["Stripe-Signature"].FirstOrDefault() ?? string.Empty;
        using var reader = new StreamReader(Request.Body, Encoding.UTF8);
        var payload = await reader.ReadToEndAsync(cancellationToken);
        var result =
            await subscriptionWebhookService.ProcessStripeWebhookAsync(signatureHeader, payload, cancellationToken);

        if (result.IsSuccess) return Ok(new { received = true });

        return result.Error switch
        {
            SubscriptionsError.WebhookSignatureInvalid => Unauthorized(new { message = result.Message }),
            SubscriptionsError.WebhookPayloadInvalid => BadRequest(new { message = result.Message }),
            _ => StatusCode(StatusCodes.Status500InternalServerError, new { message = result.Message })
        };
    }
}
