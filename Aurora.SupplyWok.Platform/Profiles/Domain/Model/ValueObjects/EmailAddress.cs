using System.Text.RegularExpressions;
namespace Aurora.SupplyWok.Platform.Profiles.Domain.Model.ValueObjects;

/// <summary>
///     Email address value object
/// </summary>
public partial record EmailAddress
{
    public string Address { get; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="EmailAddress" /> value object
    /// </summary>
    /// <param name="address">
    ///     The email address
    /// </param>
    public EmailAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address) || !EmailRegex().IsMatch(address))
            throw new ArgumentException("Invalid email address.", nameof(address));

        Address = address.Trim().ToLowerInvariant();
    }

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial Regex EmailRegex();

    public override string ToString() => Address;
}