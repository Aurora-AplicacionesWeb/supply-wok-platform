using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Aurora.SupplyWok.Platform.Subscriptions.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Subscriptions.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Subscriptions.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class ProcessedWebhookEventRepository(AppDbContext context)
    : BaseRepository<ProcessedWebhookEvent>(context), IProcessedWebhookEventRepository
{
    public async Task<bool> ExistsByStripeEventIdAsync(string stripeEventId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<ProcessedWebhookEvent>()
            .AnyAsync(processedEvent => processedEvent.StripeEventId == stripeEventId, cancellationToken);
    }
}
