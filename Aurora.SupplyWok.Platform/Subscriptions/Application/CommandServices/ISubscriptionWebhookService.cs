using Aurora.SupplyWok.Platform.Shared.Application.Model;

namespace Aurora.SupplyWok.Platform.Subscriptions.Application.CommandServices;

public interface ISubscriptionWebhookService
{
    Task<Result> ProcessStripeWebhookAsync(string signatureHeader, string payload, CancellationToken cancellationToken);
}
