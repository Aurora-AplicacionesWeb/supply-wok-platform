using Aurora.SupplyWok.Platform.Operations.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Operations.Domain.Repositories;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Aurora.SupplyWok.Platform.Operations.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class DishCategoryRepository(AppDbContext context) : BaseRepository<DishCategory>(context), IDishCategoryRepository
{
}
