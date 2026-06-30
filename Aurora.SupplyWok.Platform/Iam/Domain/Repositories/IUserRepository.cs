using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;
using Aurora.SupplyWok.Platform.Iam.Domain.Model.Aggregates;

namespace Aurora.SupplyWok.Platform.Iam.Domain.Repositories;

/**
 * <summary>
 *     The user repository
 * </summary>
 * <remarks>
 *     This repository is used to manage users
 * </remarks>
 */
public interface IUserRepository : IBaseRepository<User>
{
    /**
     * <summary>
     *     Find a user by email
     * </summary>
     * <param name="email">The email to search</param>
     * <param name="cancellationToken">The cancellation token</param>
     * <returns>The user</returns>
     */
    Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken);

    /**
     * <summary>
     *     Check if a user exists by email
     * </summary>
     * <param name="email">The email to search</param>
     * <param name="cancellationToken">The cancellation token</param>
     * <returns>True if the user exists, false otherwise</returns>
     */
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);
}