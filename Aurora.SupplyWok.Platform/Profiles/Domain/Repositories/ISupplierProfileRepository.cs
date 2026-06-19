using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Profiles.Domain.Repositories;

/// <summary>
///     Supplier profile repository interface
/// </summary>
public interface ISupplierProfileRepository: IBaseRepository<SupplierProfile>
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
    Task<SupplierProfile?> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default);
}

