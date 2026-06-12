using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Suppliers.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class SupplierRepository(AppDbContext context) : BaseRepository<Supplier>(context), ISupplierRepository
{
    public async Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await Context.Set<Supplier>().AnyAsync(supplier => supplier.Id == id, cancellationToken);
    }
}
