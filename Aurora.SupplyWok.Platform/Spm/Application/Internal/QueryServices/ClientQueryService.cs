using Aurora.SupplyWok.Platform.Spm.Application.QueryServices;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Spm.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Spm.Application.Internal.QueryServices;

/// <summary>
///     Query service that resolves client read operations for the supplier workspace.
/// </summary>
public class ClientQueryService(IClientRepository clientRepository) : IClientQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Client>> Handle(GetAllClientsBySupplierIdQuery query, CancellationToken cancellationToken)
    {
        return await clientRepository.ListBySupplierIdAsync(query.SupplierId, cancellationToken);
    }
}
