using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Suppliers.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework repository for supplier clients.
/// </summary>
public class ClientRepository(AppDbContext context) : BaseRepository<Client>(context), IClientRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<Client>> ListBySupplierIdAsync(int supplierId, CancellationToken cancellationToken)
    {
        return await (
            from client in Context.Set<Client>()
            join supplierClient in Context.Set<SupplierClient>()
                on client.Id equals supplierClient.ClientId
            where supplierClient.SupplierId == supplierId
            select client)
            .ToListAsync(cancellationToken);
    }
}
