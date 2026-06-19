using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Queries;

namespace Aurora.SupplyWok.Platform.Profiles.Application.QueryServices;

/// <summary>
///     Restaurant profile query service interface
/// </summary>
public interface IRestaurantProfileQueryService
{
    /// <summary>
    ///     Handle the retrieval of all restaurant profiles
    /// </summary>
    /// <param name="query">The <see cref="GetAllRestaurantProfilesQuery" />.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The list of all <see cref="RestaurantProfile" />.</returns>
    Task<IEnumerable<RestaurantProfile>> Handle(GetAllRestaurantProfilesQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle the retrieval of a restaurant profile by its id
    /// </summary>
    /// <param name="query">
    ///     The <see cref="GetRestaurantProfileByIdQuery" /> with the id of the restaurant profile to retrieve
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The <see cref="RestaurantProfile" /> if found, otherwise null.</returns>
    Task<RestaurantProfile?> Handle(GetRestaurantProfileByIdQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle the retrieval of a restaurant profile by its linked Iam user id
    /// </summary>
    /// <param name="query">
    ///     The <see cref="GetRestaurantProfileByUserIdQuery" /> with the Iam user id to search for
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The <see cref="RestaurantProfile" /> if found, otherwise null.</returns>
    Task<RestaurantProfile?> Handle(GetRestaurantProfileByUserIdQuery query, CancellationToken cancellationToken);
}
