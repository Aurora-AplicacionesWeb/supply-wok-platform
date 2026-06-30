using Aurora.SupplyWok.Platform.Iam.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Iam.Domain.Model.Queries;

namespace Aurora.SupplyWok.Platform.Iam.Application.QueryServices;

/**
 * <summary>
 *     The user query service interface
 * </summary>
 * <remarks>
 *     This service contract specifies handling behavior used to query users
 * </remarks>
 */
public interface IUserQueryService
{
    /**
     * <summary>
     *     Handle get user by id query
     * </summary>
     * <param name="query">The get user by id query</param>
     * <param name="cancellationToken">The cancellation token</param>
     * <returns>The user if found, null otherwise</returns>
     */
    Task<User?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken);

    /**
     * <summary>
     *     Handle get all users query
     * </summary>
     * <param name="query">The get all users query</param>
     * <param name="cancellationToken">The cancellation token</param>
     * <returns>The list of users</returns>
     */
    Task<IEnumerable<User>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken);

    /**
     * <summary>
     *     Handle get user by email query
     * </summary>
     * <param name="query">The get user by email query</param>
     * <param name="cancellationToken">The cancellation token</param>
     * <returns>The user if found, null otherwise</returns>
     */
    Task<User?> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken);
}