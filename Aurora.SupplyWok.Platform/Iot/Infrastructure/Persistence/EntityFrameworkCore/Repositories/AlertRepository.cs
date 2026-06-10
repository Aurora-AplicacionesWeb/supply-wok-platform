using Aurora.SupplyWok.Platform.Iot.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Iot.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Iot.Domain.Repositories;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Iot.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
/// Alert repository implementation
/// </summary>
/// <param name="context">The Database context</param>
public class AlertRepository(AppDbContext context) : BaseRepository<Alert>(context), IAlertRepository
{
    public async Task<Alert?> GetAlertByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await Context.Set<Alert>()
            .Include(a => ((AlertRestaurant)a).Sensor)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public new async Task<Alert?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await GetAlertByIdAsync(id, cancellationToken);
    }

    public new async Task<IEnumerable<Alert>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Set<Alert>()
            .Include(a => ((AlertRestaurant)a).Sensor)
            .ToListAsync(cancellationToken);
    }
}
