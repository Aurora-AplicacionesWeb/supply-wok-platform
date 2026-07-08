using Aurora.SupplyWok.Platform.Shared.Application.Model;
using Aurora.SupplyWok.Platform.Subscriptions.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Subscriptions.Domain.Model.Commands;

namespace Aurora.SupplyWok.Platform.Subscriptions.Application.CommandServices;

public interface ISubscriptionRegistrationCommandService
{
    Task<Result<(PendingRegistration registration, string checkoutUrl)>> Handle(
        StartSubscriptionRegistrationCommand command,
        CancellationToken cancellationToken);

    Task<Result> Handle(ProvisionSubscriptionRegistrationCommand command, CancellationToken cancellationToken);

    Task<Result> Handle(MarkPendingRegistrationExpiredCommand command, CancellationToken cancellationToken);

    Task<Result> Handle(SyncSubscriptionStatusCommand command, CancellationToken cancellationToken);
}
