using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Spm.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Spm.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework repository for supplier clients.
/// </summary>
public class ClientRepository(AppDbContext context) : BaseRepository<RestaurantReference>(context), IClientRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<RestaurantReference>> ListBySupplierIdAsync(int supplierId, CancellationToken cancellationToken)
    {
        return await (
            from client in Context.Set<RestaurantReference>()
            join supplierClient in Context.Set<SupplierRestaurant>()
                on client.Id equals supplierClient.ClientId
            where supplierClient.SupplierId == supplierId
            select client)
            .ToListAsync(cancellationToken);
    }
}
