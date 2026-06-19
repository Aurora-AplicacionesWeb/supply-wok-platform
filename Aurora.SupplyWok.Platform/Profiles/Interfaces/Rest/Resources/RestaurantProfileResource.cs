namespace Aurora.SupplyWok.Platform.Profiles.Interfaces.Rest.Resources;

/// <summary>
///     Resource representing a restaurant profile
/// </summary>
/// <param name="Id">The id of the restaurant profile.</param>
/// <param name="BusinessName">The business name of the restaurant.</param>
/// <param name="FirstName">The first name of the restaurant's contact person.</param>
/// <param name="LastName">The last name of the restaurant's contact person.</param>
/// <param name="Street">The street of the restaurant's address.</param>
/// <param name="District">The district of the restaurant's address.</param>
/// <param name="City">The city of the restaurant's address.</param>
/// <param name="Country">The country of the restaurant's address.</param>
/// <param name="ContactEmail">The contact email of the restaurant.</param>
/// <param name="Status">The current status of the restaurant profile (Active/Inactive).</param>
/// <param name="UserId">The Iam user id linked to the restaurant profile, if any.</param>
public record RestaurantProfileResource(
    int Id,
    string BusinessName,
    string FirstName,
    string LastName,
    string Street,
    string District,
    string City,
    string Country,
    string ContactEmail,
    string Status,
    int? UserId);
