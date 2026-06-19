using Aurora.SupplyWok.Platform.Profiles.Application.QueryServices;
using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Profiles.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Profiles.Application.Internal.QueryServices;

/// <summary>
///     Supplier profile query service implementation
/// </summary>
/// <remarks>
///     Implements <see cref="ISupplierProfileQueryService" /> to handle supplier profile queries
/// </remarks>
public class SupplierProfileQueryService(ISupplierProfileRepository supplierProfileRepository) : ISupplierProfileQueryService
{
    /// <summary>
    ///     Handle the retrieval of all supplier profiles
    /// </summary>
    /// <param name="query">The <see cref="GetAllSupplierProfilesQuery" />.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The list of all <see cref="SupplierProfile" />.</returns>
    public async Task<IEnumerable<SupplierProfile>> Handle(GetAllSupplierProfilesQuery query, CancellationToken cancellationToken)
    {
        return await supplierProfileRepository.ListAsync(cancellationToken);
    }

    /// <summary>
    ///     Handle the retrieval of a supplier profile by its id
    /// </summary>
    /// <param name="query">
    ///     The <see cref="GetSupplierProfileByIdQuery" /> with the id of the supplier profile to retrieve
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The <see cref="SupplierProfile" /> if found, otherwise null.</returns>
    public async Task<SupplierProfile?> Handle(GetSupplierProfileByIdQuery query, CancellationToken cancellationToken)
    {
        return await supplierProfileRepository.FindByIdAsync(query.SupplierProfileId, cancellationToken);
    }

    /// <summary>
    ///     Handle the retrieval of a supplier profile by its linked Iam user id
    /// </summary>
    /// <param name="query">
    ///     The <see cref="GetSupplierProfileByUserIdQuery" /> with the Iam user id to search for
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The <see cref="SupplierProfile" /> if found, otherwise null.</returns>
    public async Task<SupplierProfile?> Handle(GetSupplierProfileByUserIdQuery query, CancellationToken cancellationToken)
    {
        return await supplierProfileRepository.FindByUserIdAsync(query.UserId, cancellationToken);
    }
}
