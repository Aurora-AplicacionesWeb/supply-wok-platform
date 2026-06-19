using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Queries;

namespace Aurora.SupplyWok.Platform.Profiles.Application.QueryServices;

/// <summary>
///     Supplier profile query service interface
/// </summary>
public interface ISupplierProfileQueryService
{
    /// <summary>
    ///     Handle the retrieval of all supplier profiles
    /// </summary>
    /// <param name="query">The <see cref="GetAllSupplierProfilesQuery" />.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The list of all <see cref="SupplierProfile" />.</returns>
    Task<IEnumerable<SupplierProfile>> Handle(GetAllSupplierProfilesQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle the retrieval of a supplier profile by its id
    /// </summary>
    /// <param name="query">
    ///     The <see cref="GetSupplierProfileByIdQuery" /> with the id of the supplier profile to retrieve
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The <see cref="SupplierProfile" /> if found, otherwise null.</returns>
    Task<SupplierProfile?> Handle(GetSupplierProfileByIdQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle the retrieval of a supplier profile by its linked Iam user id
    /// </summary>
    /// <param name="query">
    ///     The <see cref="GetSupplierProfileByUserIdQuery" /> with the Iam user id to search for
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The <see cref="SupplierProfile" /> if found, otherwise null.</returns>
    Task<SupplierProfile?> Handle(GetSupplierProfileByUserIdQuery query, CancellationToken cancellationToken);
}
