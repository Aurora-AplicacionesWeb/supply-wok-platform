using Aurora.SupplyWok.Platform.Profiles.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Profiles.Domain.Model.Commands;

/// <summary>
///     Create Restaurant Profile Command
/// </summary>
/// <param name="BusinessName">
///     The legal/commercial name of the restaurant.
/// </param>
/// <param name="ContactName">
///     The <see cref="ValueObjects.PersonName" /> of the restaurant's contact person.
/// </param>
/// <param name="Address">
///     The <see cref="ValueObjects.StreetAddress" /> of the restaurant.
/// </param>
/// <param name="ContactEmail">
///     The <see cref="ValueObjects.EmailAddress" /> of the restaurant's contact person.
/// </param>
/// <param name="UserId">
///     Optional placeholder for a future Iam user identifier. Null while Iam is not integrated.
/// </param>
public record CreateRestaurantProfileCommand(
    string BusinessName,
    PersonName ContactName,
    StreetAddress Address,
    EmailAddress ContactEmail,
    int? UserId = null);