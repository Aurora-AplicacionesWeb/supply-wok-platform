using Aurora.SupplyWok.Platform.Shared.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Subscriptions.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Subscriptions.Domain.Model.Aggregates;

public class PendingRegistration : IAuditableEntity
{
    public PendingRegistration()
    {
        PublicId = Guid.NewGuid();
    }

    public PendingRegistration(
        string email,
        string passwordHash,
        string role,
        ESubscriptionPlanCode planCode,
        string businessName,
        string firstName,
        string lastName,
        string street,
        string district,
        string city,
        string? country,
        string contactEmail,
        string? phone,
        string? category,
        DateTimeOffset expiresAt) : this()
    {
        Email = email.Trim();
        PasswordHash = passwordHash;
        Role = role.Trim().ToLowerInvariant();
        PlanCode = planCode;
        BusinessName = businessName.Trim();
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Street = street.Trim();
        District = district.Trim();
        City = city.Trim();
        Country = string.IsNullOrWhiteSpace(country) ? "Peru" : country.Trim();
        ContactEmail = contactEmail.Trim();
        Phone = string.IsNullOrWhiteSpace(phone) ? null : phone.Trim();
        Category = string.IsNullOrWhiteSpace(category) ? null : category.Trim();
        Status = EPendingRegistrationStatus.PendingCheckout;
        ExpiresAt = expiresAt;
    }

    public int Id { get; private set; }
    public Guid PublicId { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string Role { get; private set; } = string.Empty;
    public ESubscriptionPlanCode PlanCode { get; private set; }
    public EPendingRegistrationStatus Status { get; private set; }
    public string? StripeCheckoutSessionId { get; private set; }
    public string? StripeCustomerId { get; private set; }
    public string? StripeSubscriptionId { get; private set; }
    public string BusinessName { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Street { get; private set; } = string.Empty;
    public string District { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string Country { get; private set; } = string.Empty;
    public string ContactEmail { get; private set; } = string.Empty;
    public string? Phone { get; private set; }
    public string? Category { get; private set; }
    public DateTimeOffset ExpiresAt { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public void AssignCheckoutSession(string checkoutSessionId)
    {
        StripeCheckoutSessionId = checkoutSessionId;
        Status = EPendingRegistrationStatus.PendingCheckout;
    }

    public void MarkCheckoutCompleted(string? stripeCustomerId, string? stripeSubscriptionId)
    {
        StripeCustomerId = stripeCustomerId;
        StripeSubscriptionId = stripeSubscriptionId;
        Status = EPendingRegistrationStatus.CheckoutCompleted;
    }

    public void MarkProvisioned(string? stripeCustomerId, string? stripeSubscriptionId)
    {
        StripeCustomerId = stripeCustomerId;
        StripeSubscriptionId = stripeSubscriptionId;
        Status = EPendingRegistrationStatus.Provisioned;
    }

    public void MarkExpired()
    {
        Status = EPendingRegistrationStatus.Expired;
    }

    public void MarkFailed()
    {
        Status = EPendingRegistrationStatus.Failed;
    }

    public void UpdateStripeReferences(string? stripeCustomerId, string? stripeSubscriptionId)
    {
        StripeCustomerId = stripeCustomerId;
        StripeSubscriptionId = stripeSubscriptionId;
    }
}
