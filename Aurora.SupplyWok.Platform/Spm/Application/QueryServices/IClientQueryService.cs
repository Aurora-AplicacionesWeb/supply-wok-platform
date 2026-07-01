using Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Queries;

namespace Aurora.SupplyWok.Platform.Spm.Application.QueryServices;

/// <summary>
///     Application service contract for client read operations.
/// </summary>
public interface IClientQueryService
{
    /// <summary>
    ///     Handles the query that retrieves all clients linked to a supplier.
    /// </summary>
    /// <param name="query">The client query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The clients linked to the requested supplier.</returns>
    Task<IEnumerable<RestaurantReference>> Handle(GetAllClientsBySupplierIdQuery query, CancellationToken cancellationToken);
}
