using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Profiles.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Profiles.Domain.Model.Aggregates;

/// <summary>
///     Supplier Profile Aggregate Root
/// </summary>
/// <remarks>
///     This class represents the Supplier Profile aggregate root.
///     It holds identity and business data for a supplier, decoupled from authentication.
///     <see cref="UserId" /> is an optional placeholder for future Iam integration (not yet implemented).
/// </remarks>
public partial class SupplierProfile
{
    public SupplierProfile(string businessName, PersonName contactName, StreetAddress address,
        EmailAddress contactEmail, int? userId = null) : this()
    {
        if (string.IsNullOrWhiteSpace(businessName))
            throw new ArgumentException("Business name cannot be empty.", nameof(businessName));

        BusinessName = businessName.Trim();
        ContactName = contactName;
        Address = address;
        ContactEmail = contactEmail;
        UserId = userId;
        Status = "Active";
    }

    public SupplierProfile(CreateSupplierProfileCommand command)
        : this(command.BusinessName, command.ContactName, command.Address, command.ContactEmail, command.UserId)
    {
    }

    public int Id { get; }
    public string BusinessName { get; private set; }
    public PersonName ContactName { get; private set; }
    public StreetAddress Address { get; private set; }
    public EmailAddress ContactEmail { get; private set; }
    public string Status { get; private set; }
    public int? UserId { get; private set; }

    public void UpdateContactInfo(PersonName contactName, EmailAddress contactEmail)
    {
        ContactName = contactName;
        ContactEmail = contactEmail;
    }

    public void UpdateAddress(StreetAddress address) => Address = address;
    public void Deactivate() => Status = "Inactive";
    public void Activate() => Status = "Active";
    public void LinkUser(int userId) => UserId = userId;
}