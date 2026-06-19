namespace Aurora.SupplyWok.Platform.Profiles.Interfaces.Rest.Resources;

/// <summary>
///     Resource representing a supplier profile
/// </summary>
/// <param name="Id">The id of the supplier profile.</param>
/// <param name="BusinessName">The business name of the supplier.</param>
/// <param name="FirstName">The first name of the supplier's contact person.</param>
/// <param name="LastName">The last name of the supplier's contact person.</param>
/// <param name="Street">The street of the supplier's address.</param>
/// <param name="District">The district of the supplier's address.</param>
/// <param name="City">The city of the supplier's address.</param>
/// <param name="Country">The country of the supplier's address.</param>
/// <param name="ContactEmail">The contact email of the supplier.</param>
/// <param name="Status">The current status of the supplier profile (Active/Inactive).</param>
/// <param name="UserId">The Iam user id linked to the supplier profile, if any.</param>
public record SupplierProfileResource(
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
