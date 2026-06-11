using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Aggregates;

namespace Aurora.SupplyWok.Platform.Suppliers.Domain.Repositories;

/// <summary>
///     Repository contract for supplier clients.
/// </summary>
public interface IClientRepository : IBaseRepository<Client>
{
}
