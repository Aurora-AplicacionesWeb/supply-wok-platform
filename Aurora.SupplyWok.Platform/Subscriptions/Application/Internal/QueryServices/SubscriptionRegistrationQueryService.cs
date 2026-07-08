using Aurora.SupplyWok.Platform.Subscriptions.Application.QueryServices;
using Aurora.SupplyWok.Platform.Subscriptions.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Subscriptions.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Subscriptions.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Subscriptions.Application.Internal.QueryServices;

public class SubscriptionRegistrationQueryService(
    IPendingRegistrationRepository pendingRegistrationRepository,
    ISubscriptionRepository subscriptionRepository) : ISubscriptionRegistrationQueryService
{
    public async Task<PendingRegistration?> Handle(GetPendingRegistrationByPublicIdQuery query,
        CancellationToken cancellationToken)
    {
        return await pendingRegistrationRepository.FindByPublicIdAsync(query.PublicId, cancellationToken);
    }

    public async Task<PendingRegistration?> Handle(GetPendingRegistrationByStripeSessionIdQuery query,
        CancellationToken cancellationToken)
    {
        return await pendingRegistrationRepository.FindByStripeCheckoutSessionIdAsync(query.StripeSessionId,
            cancellationToken);
    }

    public async Task<Subscription?> Handle(GetSubscriptionByUserIdQuery query, CancellationToken cancellationToken)
    {
        return await subscriptionRepository.FindByUserIdAsync(query.UserId, cancellationToken);
    }

    public async Task<PendingRegistration?> Handle(GetRegistrationStatusQuery query, CancellationToken cancellationToken)
    {
        return await pendingRegistrationRepository.FindByPublicIdAsync(query.PublicId, cancellationToken);
    }
}
