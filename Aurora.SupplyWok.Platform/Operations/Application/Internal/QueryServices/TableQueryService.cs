using Aurora.SupplyWok.Platform.Operations.Application.QueryServices;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Operations.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Operations.Application.Internal.QueryServices;

public class TableQueryService (ITableRepository tableRepository) : ITableQueryService
{
    public async Task<IEnumerable<Table>> Handle(GetAllTablesQuery query, CancellationToken cancellationToken)
    {
        return await tableRepository.ListAsync(cancellationToken);
    }

    public async Task<Table?> Handle(GetTableByIdQuery query, CancellationToken cancellationToken)
    {
        return await tableRepository.GetTableByIdAsync(query.TableId, cancellationToken);
    }
    
}