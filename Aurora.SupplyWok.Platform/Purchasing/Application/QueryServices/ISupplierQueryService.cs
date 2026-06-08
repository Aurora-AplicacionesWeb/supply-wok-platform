using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Queries;

namespace Aurora.SupplyWok.Platform.Purchasing.Application.QueryServices;

public interface ISupplierQueryService
{
    Task<IEnumerable<Supplier>> Handle(GetAllSuppliersQuery query, CancellationToken cancellationToken);
}
