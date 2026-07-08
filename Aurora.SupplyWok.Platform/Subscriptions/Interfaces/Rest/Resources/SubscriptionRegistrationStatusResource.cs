namespace Aurora.SupplyWok.Platform.Subscriptions.Interfaces.Rest.Resources;

public record SubscriptionRegistrationStatusResource(
    Guid RegistrationId,
    string Status,
    string Email,
    string Role,
    string PlanCode,
    bool CanRetryCheckout,
    bool ReadyForLogin);
