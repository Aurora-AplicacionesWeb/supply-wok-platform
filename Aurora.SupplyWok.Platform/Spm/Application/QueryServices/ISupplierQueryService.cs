using Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Queries;

namespace Aurora.SupplyWok.Platform.Spm.Application.QueryServices;

public interface ISupplierQueryService
{
    Task<IEnumerable<Supplier>> Handle(GetAllSuppliersQuery query, CancellationToken cancellationToken);
}
