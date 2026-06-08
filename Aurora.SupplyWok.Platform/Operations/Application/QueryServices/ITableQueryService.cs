using Aurora.SupplyWok.Platform.Operations.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.Queries;
namespace Aurora.SupplyWok.Platform.Operations.Application.QueryServices;

/// <summary>
/// Table query service interface
/// </summary>
public interface ITableQueryService
{
    Task<IEnumerable<Table>> Handle(GetAllTablesQuery query, CancellationToken cancellationToken);
    Task<Table?> Handle(GetTableByIdQuery query, CancellationToken cancellationToken);
}