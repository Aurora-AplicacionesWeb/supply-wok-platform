namespace Aurora.SupplyWok.Platform.Profiles.Domain.Model.ValueObjects;

/// <summary>
///     Value object for street address
/// </summary>
public record StreetAddress
{
    public string Street { get; }
    public string District { get; }
    public string City { get; }
    public string Country { get; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="StreetAddress" /> value object
    /// </summary>
    /// <param name="street">
    ///     The street name
    /// </param>
    /// <param name="district">
    ///     The district name
    /// </param>
    /// <param name="city">
    ///     The city name
    /// </param>
    /// <param name="country">
    ///     The country name
    /// </param>
    public StreetAddress(string street, string district, string city, string country)
    {
        if (string.IsNullOrWhiteSpace(street)) throw new ArgumentException("Street cannot be empty.", nameof(street));
        if (string.IsNullOrWhiteSpace(district)) throw new ArgumentException("District cannot be empty.", nameof(district));
        if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City cannot be empty.", nameof(city));
        if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException("Country cannot be empty.", nameof(country));

        Street = street.Trim();
        District = district.Trim();
        City = city.Trim();
        Country = country.Trim();
    }

    public override string ToString() => $"{Street}, {District}, {City}, {Country}";
}