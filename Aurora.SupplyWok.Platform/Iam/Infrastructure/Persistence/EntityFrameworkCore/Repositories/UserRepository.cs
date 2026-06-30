using Aurora.SupplyWok.Platform.Iam.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Iam.Domain.Repositories;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/**
 * <summary>
 *     The user repository
 * </summary>
 * <remarks>
 *     This repository is used to manage users
 * </remarks>
 */
public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository
{
    /**
     * <summary>
     *     Find a user by email
     * </summary>
     * <param name="email">The email to search</param>
     * <param name="cancellationToken">The cancellation token</param>
     * <returns>The user</returns>
     */
    public async Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(user => user.Email.Equals(email), cancellationToken);
    }

    /**
     * <summary>
     *     Check if a user exists by email
     * </summary>
     * <param name="email">The email to search</param>
     * <param name="cancellationToken">The cancellation token</param>
     * <returns>True if the user exists, false otherwise</returns>
     */
    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await Context.Set<User>().AnyAsync(user => user.Email.Equals(email), cancellationToken);
    }
}