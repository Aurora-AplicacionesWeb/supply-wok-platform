using Aurora.SupplyWok.Platform.Shared.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Subscriptions.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Subscriptions.Domain.Model.Aggregates;

public class Subscription : IAuditableEntity
{
    public Subscription()
    {
    }

    public Subscription(
        int userId,
        string role,
        ESubscriptionPlanCode planCode,
        ESubscriptionStatus status,
        string stripeCustomerId,
        string stripeSubscriptionId,
        string stripePriceId,
        DateTimeOffset? currentPeriodStart,
        DateTimeOffset? currentPeriodEnd)
    {
        UserId = userId;
        Role = role.Trim().ToLowerInvariant();
        PlanCode = planCode;
        Status = status;
        StripeCustomerId = stripeCustomerId;
        StripeSubscriptionId = stripeSubscriptionId;
        StripePriceId = stripePriceId;
        CurrentPeriodStart = currentPeriodStart;
        CurrentPeriodEnd = currentPeriodEnd;
    }

    public int Id { get; private set; }
    public int UserId { get; private set; }
    public string Role { get; private set; } = string.Empty;
    public ESubscriptionPlanCode PlanCode { get; private set; }
    public ESubscriptionStatus Status { get; private set; }
    public string StripeCustomerId { get; private set; } = string.Empty;
    public string StripeSubscriptionId { get; private set; } = string.Empty;
    public string StripePriceId { get; private set; } = string.Empty;
    public DateTimeOffset? CurrentPeriodStart { get; private set; }
    public DateTimeOffset? CurrentPeriodEnd { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public void SyncStatus(
        ESubscriptionStatus status,
        ESubscriptionPlanCode planCode,
        string stripePriceId,
        DateTimeOffset? currentPeriodStart,
        DateTimeOffset? currentPeriodEnd)
    {
        Status = status;
        PlanCode = planCode;
        StripePriceId = stripePriceId;
        CurrentPeriodStart = currentPeriodStart;
        CurrentPeriodEnd = currentPeriodEnd;
    }
}
