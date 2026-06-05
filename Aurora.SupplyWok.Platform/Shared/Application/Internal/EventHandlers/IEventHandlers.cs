using Acme.Center.Platform.Shared.Domain.Model.Events;
using Cortex.Mediator.Notifications;

namespace Aurora.SupplyWok.Platform.Application.Model;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}