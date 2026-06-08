using Aurora.SupplyWok.Platform.Operations.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Operations.Domain.Repositories;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Microsoft.EntityFrameworkCore;


namespace Aurora.SupplyWok.Platform.Operations.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class TableRepository(AppDbContext context) : BaseRepository<Table>(context), ITableRepository
{
    public async Task<Table?> GetTableByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await Context.Set<Table>().FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }
    
}