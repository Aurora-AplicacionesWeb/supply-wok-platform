using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Aurora.SupplyWok.Platform.Subscriptions.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Subscriptions.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Subscriptions.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class SubscriptionRepository(AppDbContext context)
    : BaseRepository<Subscription>(context), ISubscriptionRepository
{
    public async Task<Subscription?> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Subscription>()
            .FirstOrDefaultAsync(subscription => subscription.UserId == userId, cancellationToken);
    }

    public async Task<Subscription?> FindByStripeSubscriptionIdAsync(string stripeSubscriptionId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Subscription>()
            .FirstOrDefaultAsync(subscription => subscription.StripeSubscriptionId == stripeSubscriptionId,
                cancellationToken);
    }
}
