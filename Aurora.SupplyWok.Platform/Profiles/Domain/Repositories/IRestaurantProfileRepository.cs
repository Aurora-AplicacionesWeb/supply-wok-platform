using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Profiles.Domain.Repositories;

/// <summary>
///     Restaurant profile repository interface
/// </summary>
public interface IRestaurantProfileRepository : IBaseRepository<RestaurantProfile>
{
    /// <summary>
    ///     Find a restaurant profile by its linked Iam user id
    /// </summary>
    /// <param name="userId">
    ///     The Iam user id to search for
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The <see cref="RestaurantProfile" /> if found, otherwise null
    /// </returns>
    Task<RestaurantProfile?> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default);
}

