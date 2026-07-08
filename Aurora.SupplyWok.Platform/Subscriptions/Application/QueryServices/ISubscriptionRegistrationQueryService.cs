using Aurora.SupplyWok.Platform.Subscriptions.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Subscriptions.Domain.Model.Queries;

namespace Aurora.SupplyWok.Platform.Subscriptions.Application.QueryServices;

public interface ISubscriptionRegistrationQueryService
{
    Task<PendingRegistration?> Handle(GetPendingRegistrationByPublicIdQuery query, CancellationToken cancellationToken);
    Task<PendingRegistration?> Handle(GetPendingRegistrationByStripeSessionIdQuery query, CancellationToken cancellationToken);
    Task<Subscription?> Handle(GetSubscriptionByUserIdQuery query, CancellationToken cancellationToken);
    Task<PendingRegistration?> Handle(GetRegistrationStatusQuery query, CancellationToken cancellationToken);
}
