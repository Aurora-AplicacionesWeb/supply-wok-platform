namespace Aurora.SupplyWok.Platform.Profiles.Interfaces.Acl;

/// <summary>
///     Profiles context facade interface
/// </summary>
/// <remarks>
///     Anti-corruption layer exposed to other bounded contexts. Methods only accept and return
///     primitive types so other contexts never depend on Profiles' value objects or aggregates directly.
/// </remarks>
public interface IProfilesContextFacade
{
    /// <summary>
    ///     Create a new restaurant profile
    /// </summary>
    /// <param name="businessName">The business name of the restaurant.</param>
    /// <param name="firstName">The first name of the restaurant's contact person.</param>
    /// <param name="lastName">The last name of the restaurant's contact person.</param>
    /// <param name="street">The street of the restaurant's address.</param>
    /// <param name="district">The district of the restaurant's address.</param>
    /// <param name="city">The city of the restaurant's address.</param>
    /// <param name="country">The country of the restaurant's address.</param>
    /// <param name="contactEmail">The contact email of the restaurant.</param>
    /// <param name="userId">The optional Iam user id to link to the restaurant profile.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The id of the created restaurant profile, or 0 if the operation failed.</returns>
    Task<int> CreateRestaurantProfile(string businessName, string firstName, string lastName, string street,
        string district, string city, string country, string contactEmail, int? userId,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Create a new supplier profile
    /// </summary>
    /// <param name="businessName">The business name of the supplier.</param>
    /// <param name="firstName">The first name of the supplier's contact person.</param>
    /// <param name="lastName">The last name of the supplier's contact person.</param>
    /// <param name="street">The street of the supplier's address.</param>
    /// <param name="district">The district of the supplier's address.</param>
    /// <param name="city">The city of the supplier's address.</param>
    /// <param name="country">The country of the supplier's address.</param>
    /// <param name="contactEmail">The contact email of the supplier.</param>
    /// <param name="userId">The optional Iam user id to link to the supplier profile.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The id of the created supplier profile, or 0 if the operation failed.</returns>
    Task<int> CreateSupplierProfile(string businessName, string firstName, string lastName, string street,
        string district, string city, string country, string contactEmail, int? userId,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Fetch the id of a restaurant profile by its linked Iam user id
    /// </summary>
    /// <param name="userId">The Iam user id to search for.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The id of the restaurant profile if found, otherwise 0.</returns>
    Task<int> FetchRestaurantProfileIdByUserId(int userId, CancellationToken cancellationToken);

    /// <summary>
    ///     Fetch the id of a supplier profile by its linked Iam user id
    /// </summary>
    /// <param name="userId">The Iam user id to search for.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The id of the supplier profile if found, otherwise 0.</returns>
    Task<int> FetchSupplierProfileIdByUserId(int userId, CancellationToken cancellationToken);

    /// <summary>
    ///     Fetch the business name of a supplier profile by its id
    /// </summary>
    /// <param name="supplierProfileId">The supplier profile identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The business name of the supplier profile if found; otherwise null.</returns>
    Task<string?> FetchSupplierProfileNameById(int supplierProfileId, CancellationToken cancellationToken);

    Task<SupplierProfileAclResource?> GetSupplierProfileById(int supplierProfileId,
        CancellationToken cancellationToken);

    Task<RestaurantProfileAclResource?> GetRestaurantProfileById(int restaurantProfileId,
        CancellationToken cancellationToken);

    Task<IEnumerable<SupplierProfileAclResource>> GetSupplierProfilesByIds(IEnumerable<int> supplierProfileIds,
        CancellationToken cancellationToken);

    Task<IEnumerable<RestaurantProfileAclResource>> GetRestaurantProfilesByIds(IEnumerable<int> restaurantProfileIds,
        CancellationToken cancellationToken);

    Task<SupplierIdentityAclResource?> GetSupplierIdentityById(int supplierProfileId,
        CancellationToken cancellationToken);
}
