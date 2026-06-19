using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Profiles.Domain.Repositories;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Profiles.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Supplier profile repository implementation
/// </summary>
/// <remarks>
///     Implements <see cref="ISupplierProfileRepository" /> using Entity Framework Core.
/// </remarks>
public class SupplierProfileRepository(AppDbContext context)
    : BaseRepository<SupplierProfile>(context), ISupplierProfileRepository
{
    /// <summary>
    ///     Find a supplier profile by its linked Iam user id
    /// </summary>
    /// <param name="userId">
    ///     The Iam user id to search for
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The <see cref="SupplierProfile" /> if found, otherwise null
    /// </returns>
    public async Task<SupplierProfile?> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<SupplierProfile>()
            .FirstOrDefaultAsync(profile => profile.UserId == userId, cancellationToken);
    }
}
