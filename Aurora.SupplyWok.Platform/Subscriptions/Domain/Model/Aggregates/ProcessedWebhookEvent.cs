namespace Aurora.SupplyWok.Platform.Subscriptions.Domain.Model.Aggregates;

public class ProcessedWebhookEvent
{
    public ProcessedWebhookEvent()
    {
    }

    public ProcessedWebhookEvent(string stripeEventId, string eventType)
    {
        StripeEventId = stripeEventId;
        EventType = eventType;
        ProcessedAt = DateTimeOffset.UtcNow;
    }

    public int Id { get; private set; }
    public string StripeEventId { get; private set; } = string.Empty;
    public string EventType { get; private set; } = string.Empty;
    public DateTimeOffset ProcessedAt { get; private set; }
}
