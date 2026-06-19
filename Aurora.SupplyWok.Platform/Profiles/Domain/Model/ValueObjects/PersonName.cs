namespace Aurora.SupplyWok.Platform.Profiles.Domain.Model.ValueObjects;

/// <summary>
///     Value object for a person's name
/// </summary>
public record PersonName
{
    public string FirstName { get; }
    public string LastName { get; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="PersonName" /> value object
    /// </summary>
    /// <param name="firstName">
    ///     The first name of the person
    /// </param>
    /// <param name="lastName">
    ///     The last name of the person
    /// </param>
    public PersonName(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be empty.", nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be empty.", nameof(lastName));

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
    }

    public string FullName => $"{FirstName} {LastName}";
}