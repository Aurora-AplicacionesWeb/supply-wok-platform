namespace Aurora.SupplyWok.Platform.Subscriptions.Interfaces.Rest.Resources;

public record SubscriptionRegistrationResource(
    Guid RegistrationId,
    string Status,
    string CheckoutUrl);
