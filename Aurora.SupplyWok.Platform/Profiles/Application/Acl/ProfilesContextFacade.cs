using Aurora.SupplyWok.Platform.Profiles.Application.CommandServices;
using Aurora.SupplyWok.Platform.Profiles.Application.QueryServices;
using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Profiles.Domain.Model.ValueObjects;
using Aurora.SupplyWok.Platform.Profiles.Interfaces.Acl;

namespace Aurora.SupplyWok.Platform.Profiles.Application.Acl;

/// <summary>
///     Profiles context facade implementation
/// </summary>
/// <remarks>
///     Implements <see cref="IProfilesContextFacade" />. Builds the Domain Commands/Queries (with their
///     Value Objects) internally from the primitive parameters received, and delegates to the
///     corresponding Command/Query services.
/// </remarks>
public class ProfilesContextFacade(
    IRestaurantProfileCommandService restaurantProfileCommandService,
    ISupplierProfileCommandService supplierProfileCommandService,
    IRestaurantProfileQueryService restaurantProfileQueryService,
    ISupplierProfileQueryService supplierProfileQueryService) : IProfilesContextFacade
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
    public async Task<int> CreateRestaurantProfile(string businessName, string firstName, string lastName,
        string street, string district, string city, string country, string contactEmail, int? userId,
        CancellationToken cancellationToken)
    {
        var command = new CreateRestaurantProfileCommand(
            businessName,
            new PersonName(firstName, lastName),
            new StreetAddress(street, district, city, country),
            new EmailAddress(contactEmail),
            userId);

        var result = await restaurantProfileCommandService.Handle(command, cancellationToken);
        return result.IsSuccess ? result.Value!.Id : 0;
    }

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
    public async Task<int> CreateSupplierProfile(string businessName, string firstName, string lastName,
        string street, string district, string city, string country, string contactEmail, int? userId,
        CancellationToken cancellationToken)
    {
        var command = new CreateSupplierProfileCommand(
            businessName,
            new PersonName(firstName, lastName),
            new StreetAddress(street, district, city, country),
            new EmailAddress(contactEmail),
            userId);

        var result = await supplierProfileCommandService.Handle(command, cancellationToken);
        return result.IsSuccess ? result.Value!.Id : 0;
    }

    /// <summary>
    ///     Fetch the id of a restaurant profile by its linked Iam user id
    /// </summary>
    /// <param name="userId">The Iam user id to search for.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The id of the restaurant profile if found, otherwise 0.</returns>
    public async Task<int> FetchRestaurantProfileIdByUserId(int userId, CancellationToken cancellationToken)
    {
        var query = new GetRestaurantProfileByUserIdQuery(userId);
        var restaurantProfile = await restaurantProfileQueryService.Handle(query, cancellationToken);
        return restaurantProfile?.Id ?? 0;
    }

    /// <summary>
    ///     Fetch the id of a supplier profile by its linked Iam user id
    /// </summary>
    /// <param name="userId">The Iam user id to search for.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The id of the supplier profile if found, otherwise 0.</returns>
    public async Task<int> FetchSupplierProfileIdByUserId(int userId, CancellationToken cancellationToken)
    {
        var query = new GetSupplierProfileByUserIdQuery(userId);
        var supplierProfile = await supplierProfileQueryService.Handle(query, cancellationToken);
        return supplierProfile?.Id ?? 0;
    }

    /// <inheritdoc />
    public async Task<string?> FetchSupplierProfileNameById(int supplierProfileId, CancellationToken cancellationToken)
    {
        var query = new GetSupplierProfileByIdQuery(supplierProfileId);
        var supplierProfile = await supplierProfileQueryService.Handle(query, cancellationToken);
        return supplierProfile?.BusinessName;
    }
}
