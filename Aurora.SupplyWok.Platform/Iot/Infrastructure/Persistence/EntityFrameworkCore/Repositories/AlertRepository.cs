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
        var restaurantAlert = await Context.Set<AlertRestaurant>()
            .Include(a => a.Sensor)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        if (restaurantAlert != null)
            return restaurantAlert;

        return await Context.Set<AlertSupplier>()
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Alert>> ListRestaurantAlertsAsync(CancellationToken cancellationToken)
    {
        return await Context.Set<AlertRestaurant>()
            .Include(a => a.Sensor)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Alert>> ListSupplierAlertsAsync(CancellationToken cancellationToken)
    {
        return await Context.Set<AlertSupplier>()
            .ToListAsync(cancellationToken);
    }

    public new async Task<Alert?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await GetAlertByIdAsync(id, cancellationToken);
    }

    public new async Task<IEnumerable<Alert>> ListAsync(CancellationToken cancellationToken = default)
    {
        var restaurantAlerts = await Context.Set<AlertRestaurant>()
            .Include(a => a.Sensor)
            .ToListAsync(cancellationToken);

        var supplierAlerts = await Context.Set<AlertSupplier>()
            .ToListAsync(cancellationToken);

        return restaurantAlerts
            .Cast<Alert>()
            .Concat(supplierAlerts)
            .OrderBy(a => a.Id);
    }
}
