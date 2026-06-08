using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Purchasing.Domain.Repositories;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Purchasing.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class SupplierRepository(AppDbContext context) : BaseRepository<Supplier>(context), ISupplierRepository
{
    public async Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await Context.Set<Supplier>().AnyAsync(supplier => supplier.Id == id, cancellationToken);
    }
}
