namespace Aurora.SupplyWok.Platform.Subscriptions.Domain.Model.Commands;

public record MarkPendingRegistrationExpiredCommand(Guid PendingRegistrationPublicId);
