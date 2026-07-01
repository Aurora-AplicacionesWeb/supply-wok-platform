using Aurora.SupplyWok.Platform.Spm.Application.QueryServices;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Spm.Interfaces.Acl;

namespace Aurora.SupplyWok.Platform.Spm.Application.Acl;

/// <summary>
/// Supplier-facing facade that exposes client data through the ACL.
/// </summary>
public class SupplierClientsContextFacade(IClientQueryService clientQueryService) : ISupplierClientsContextFacade
{
    /// <inheritdoc />
    public async Task<IEnumerable<ClientAclResource>> GetClientsBySupplierId(int supplierId, CancellationToken cancellationToken)
    {
        var clients = await clientQueryService.Handle(new GetAllClientsBySupplierIdQuery(supplierId), cancellationToken);
        return clients.Select(client => new ClientAclResource(
            client.Id,
            client.Name,
            client.District,
            client.Status));
    }
}
