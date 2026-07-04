namespace Aurora.SupplyWok.Platform.Profiles.Interfaces.Rest.Resources;

/// <summary>
///     Resource for updating an existing supplier profile
/// </summary>
/// <param name="BusinessName">The business name of the supplier.</param>
/// <param name="FirstName">The first name of the supplier's contact person.</param>
/// <param name="LastName">The last name of the supplier's contact person.</param>
/// <param name="Street">The street of the supplier's address.</param>
/// <param name="District">The district of the supplier's address.</param>
/// <param name="City">The city of the supplier's address.</param>
/// <param name="Country">The country of the supplier's address.</param>
/// <param name="ContactEmail">The contact email of the supplier.</param>
/// <param name="Phone">The supplier contact phone.</param>
/// <param name="Category">The supplier category.</param>
public record UpdateSupplierProfileResource(
    string BusinessName,
    string FirstName,
    string LastName,
    string Street,
    string District,
    string City,
    string Country,
    string ContactEmail,
    string Phone,
    string Category);
