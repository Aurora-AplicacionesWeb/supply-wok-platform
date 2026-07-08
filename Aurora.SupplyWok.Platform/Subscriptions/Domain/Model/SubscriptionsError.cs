namespace Aurora.SupplyWok.Platform.Subscriptions.Domain.Model;

public enum SubscriptionsError
{
    InvalidPlan,
    InvalidRole,
    InvalidData,
    DuplicateEmail,
    RegistrationNotFound,
    RegistrationExpired,
    RegistrationAlreadyProvisioned,
    StripeConfigurationMissing,
    StripeSessionCreationFailed,
    WebhookSignatureInvalid,
    WebhookPayloadInvalid,
    WebhookProcessingFailed,
    SubscriptionNotFound,
    ProvisioningFailed,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
