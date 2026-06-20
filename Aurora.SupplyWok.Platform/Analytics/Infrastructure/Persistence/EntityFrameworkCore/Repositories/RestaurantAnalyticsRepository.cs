using Aurora.SupplyWok.Platform.Analytics.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Analytics.Domain.Repositories;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Aurora.SupplyWok.Platform.Analytics.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class RestaurantAnalyticsRepository(AppDbContext context) : BaseRepository<RestaurantAnalytics>(context), IRestaurantAnalyticsRepository
{
}
