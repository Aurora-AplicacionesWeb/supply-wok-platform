using Aurora.SupplyWok.Platform.Suppliers.Application.QueryServices;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Suppliers.Application.Internal.QueryServices;

/// <summary>
///     Query service that resolves client read operations for the supplier workspace.
/// </summary>
public class ClientQueryService(IClientRepository clientRepository) : IClientQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Client>> Handle(GetAllClientsQuery query, CancellationToken cancellationToken)
    {
        return await clientRepository.ListAsync(cancellationToken);
    }
}
