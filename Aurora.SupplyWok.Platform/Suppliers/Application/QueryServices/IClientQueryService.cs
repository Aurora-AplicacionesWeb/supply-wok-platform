using Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Queries;

namespace Aurora.SupplyWok.Platform.Suppliers.Application.QueryServices;

/// <summary>
///     Application service contract for client read operations.
/// </summary>
public interface IClientQueryService
{
    /// <summary>
    ///     Handles the query that retrieves all clients.
    /// </summary>
    /// <param name="query">The client query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The clients available to the supplier workspace.</returns>
    Task<IEnumerable<Client>> Handle(GetAllClientsQuery query, CancellationToken cancellationToken);
}
