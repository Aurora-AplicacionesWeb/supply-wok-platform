using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Profiles.Domain.Repositories;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Profiles.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Restaurant profile repository implementation
/// </summary>
/// <remarks>
///     Implements <see cref="IRestaurantProfileRepository" /> using Entity Framework Core.
/// </remarks>
public class RestaurantProfileRepository(AppDbContext context)
    : BaseRepository<RestaurantProfile>(context), IRestaurantProfileRepository
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
    public async Task<RestaurantProfile?> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<RestaurantProfile>()
            .FirstOrDefaultAsync(profile => profile.UserId == userId, cancellationToken);
    }
}
