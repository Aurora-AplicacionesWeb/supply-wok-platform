using Aurora.SupplyWok.Platform.Profiles.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Profiles.Domain.Model.Commands;

/// <summary>
///     Update Restaurant Profile Command
/// </summary>
/// <param name="Id">The restaurant profile identifier.</param>
/// <param name="BusinessName">The business name of the restaurant.</param>
/// <param name="ContactName">The contact person name.</param>
/// <param name="Address">The restaurant address.</param>
/// <param name="ContactEmail">The contact email.</param>
public record UpdateRestaurantProfileCommand(
    int Id,
    string BusinessName,
    PersonName ContactName,
    StreetAddress Address,
    EmailAddress ContactEmail);
