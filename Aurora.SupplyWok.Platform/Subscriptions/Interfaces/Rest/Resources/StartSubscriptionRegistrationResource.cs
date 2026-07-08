namespace Aurora.SupplyWok.Platform.Subscriptions.Interfaces.Rest.Resources;

public record StartSubscriptionRegistrationResource(
    string Email,
    string Password,
    string Role,
    string PlanCode,
    string BusinessName,
    string FirstName,
    string LastName,
    string Street,
    string District,
    string City,
    string? Country,
    string ContactEmail,
    string? Phone,
    string? Category);
