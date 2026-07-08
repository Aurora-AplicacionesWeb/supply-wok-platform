using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;
using Aurora.SupplyWok.Platform.Subscriptions.Domain.Model.Aggregates;

namespace Aurora.SupplyWok.Platform.Subscriptions.Domain.Repositories;

public interface IPendingRegistrationRepository : IBaseRepository<PendingRegistration>
{
    Task<PendingRegistration?> FindByPublicIdAsync(Guid publicId, CancellationToken cancellationToken = default);
    Task<PendingRegistration?> FindByStripeCheckoutSessionIdAsync(string stripeCheckoutSessionId, CancellationToken cancellationToken = default);
    Task<PendingRegistration?> FindByStripeSubscriptionIdAsync(string stripeSubscriptionId, CancellationToken cancellationToken = default);
}
