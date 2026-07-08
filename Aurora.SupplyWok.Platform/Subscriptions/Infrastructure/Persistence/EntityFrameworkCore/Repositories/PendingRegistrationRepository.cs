using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Aurora.SupplyWok.Platform.Subscriptions.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Subscriptions.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Subscriptions.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class PendingRegistrationRepository(AppDbContext context)
    : BaseRepository<PendingRegistration>(context), IPendingRegistrationRepository
{
    public async Task<PendingRegistration?> FindByPublicIdAsync(Guid publicId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<PendingRegistration>()
            .FirstOrDefaultAsync(registration => registration.PublicId == publicId, cancellationToken);
    }

    public async Task<PendingRegistration?> FindByStripeCheckoutSessionIdAsync(string stripeCheckoutSessionId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<PendingRegistration>()
            .FirstOrDefaultAsync(registration => registration.StripeCheckoutSessionId == stripeCheckoutSessionId,
                cancellationToken);
    }

    public async Task<PendingRegistration?> FindByStripeSubscriptionIdAsync(string stripeSubscriptionId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<PendingRegistration>()
            .FirstOrDefaultAsync(registration => registration.StripeSubscriptionId == stripeSubscriptionId,
                cancellationToken);
    }
}
