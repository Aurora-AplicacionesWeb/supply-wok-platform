namespace Aurora.SupplyWok.Platform.Profiles.Interfaces.Rest.Resources;

/// <summary>
///     Resource for updating an existing restaurant profile
/// </summary>
/// <param name="BusinessName">The business name of the restaurant.</param>
/// <param name="FirstName">The first name of the restaurant's contact person.</param>
/// <param name="LastName">The last name of the restaurant's contact person.</param>
/// <param name="Street">The street of the restaurant's address.</param>
/// <param name="District">The district of the restaurant's address.</param>
/// <param name="City">The city of the restaurant's address.</param>
/// <param name="Country">The country of the restaurant's address.</param>
/// <param name="ContactEmail">The contact email of the restaurant.</param>
public record UpdateRestaurantProfileResource(
    string BusinessName,
    string FirstName,
    string LastName,
    string Street,
    string District,
    string City,
    string Country,
    string ContactEmail);
