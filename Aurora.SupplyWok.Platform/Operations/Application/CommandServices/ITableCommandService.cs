using Aurora.SupplyWok.Platform.Operations.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Shared.Application.Model;
namespace Aurora.SupplyWok.Platform.Operations.Application.CommandServices;

/// <summary>
/// Table command service interface
/// </summary>
public interface ITableCommandService
{
    Task<Result<Table>> Handle(CreateTableCommand command, CancellationToken cancellationToken);
    Task<Result<Table>> Handle(UpdateTableCommand command, CancellationToken cancellationToken);
    Task<Result<bool>> Handle(DeleteTableCommand command, CancellationToken cancellationToken);
}