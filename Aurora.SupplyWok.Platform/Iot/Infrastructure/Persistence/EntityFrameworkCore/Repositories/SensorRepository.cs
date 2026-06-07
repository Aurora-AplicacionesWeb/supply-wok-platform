using Aurora.SupplyWok.Platform.Iot.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Iot.Domain.Repositories;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Iot.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
/// Sensor repository implementation
/// </summary>
/// <param name="context">The Database context</param>
public class SensorRepository(AppDbContext context) : BaseRepository<Sensor>(context), ISensorRepository
{
    public async Task<Sensor?> GetSensorByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await Context.Set<Sensor>().FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }
}