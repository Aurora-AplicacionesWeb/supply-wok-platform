using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;
using Aurora.SupplyWok.Platform.Subscriptions.Domain.Model.Aggregates;

namespace Aurora.SupplyWok.Platform.Subscriptions.Domain.Repositories;

public interface IProcessedWebhookEventRepository : IBaseRepository<ProcessedWebhookEvent>
{
    Task<bool> ExistsByStripeEventIdAsync(string stripeEventId, CancellationToken cancellationToken = default);
}
