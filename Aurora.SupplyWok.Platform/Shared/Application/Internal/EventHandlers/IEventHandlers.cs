using Cortex.Mediator.Notifications;
using Aurora.SupplyWok.Platform.Shared.Domain.Model.Events;

namespace Aurora.SupplyWok.Platform.Shared.Application.Internal.EventHandlers;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}