using Aurora.SupplyWok.Platform.Profiles.Application.QueryServices;
using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Profiles.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Profiles.Application.Internal.QueryServices;

/// <summary>
///     Restaurant profile query service implementation
/// </summary>
/// <remarks>
///     Implements <see cref="IRestaurantProfileQueryService" /> to handle restaurant profile queries
/// </remarks>
public class RestaurantProfileQueryService(IRestaurantProfileRepository restaurantProfileRepository) : IRestaurantProfileQueryService
{
    /// <summary>
    ///     Handle the retrieval of all restaurant profiles
    /// </summary>
    /// <param name="query">The <see cref="GetAllRestaurantProfilesQuery" />.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The list of all <see cref="RestaurantProfile" />.</returns>
    public async Task<IEnumerable<RestaurantProfile>> Handle(GetAllRestaurantProfilesQuery query, CancellationToken cancellationToken)
    {
        return await restaurantProfileRepository.ListAsync(cancellationToken);
    }

    /// <summary>
    ///     Handle the retrieval of a restaurant profile by its id
    /// </summary>
    /// <param name="query">
    ///     The <see cref="GetRestaurantProfileByIdQuery" /> with the id of the restaurant profile to retrieve
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The <see cref="RestaurantProfile" /> if found, otherwise null.</returns>
    public async Task<RestaurantProfile?> Handle(GetRestaurantProfileByIdQuery query, CancellationToken cancellationToken)
    {
        return await restaurantProfileRepository.FindByIdAsync(query.RestaurantProfileId, cancellationToken);
    }

    /// <summary>
    ///     Handle the retrieval of a restaurant profile by its linked Iam user id
    /// </summary>
    /// <param name="query">
    ///     The <see cref="GetRestaurantProfileByUserIdQuery" /> with the Iam user id to search for
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The <see cref="RestaurantProfile" /> if found, otherwise null.</returns>
    public async Task<RestaurantProfile?> Handle(GetRestaurantProfileByUserIdQuery query, CancellationToken cancellationToken)
    {
        return await restaurantProfileRepository.FindByUserIdAsync(query.UserId, cancellationToken);
    }
}
