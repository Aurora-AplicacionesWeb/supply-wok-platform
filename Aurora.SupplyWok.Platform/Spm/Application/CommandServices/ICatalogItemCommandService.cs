using Aurora.SupplyWok.Platform.Shared.Application.Model;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Commands;

namespace Aurora.SupplyWok.Platform.Spm.Application.CommandServices;

public interface ICatalogItemCommandService
{
    Task<Result<CatalogItem>> Handle(CreateCatalogItemCommand command, CancellationToken cancellationToken);
    Task<Result<CatalogItem>> Handle(UpdateCatalogItemCommand command, CancellationToken cancellationToken);
    Task<Result<bool>> Handle(DeleteCatalogItemCommand command, CancellationToken cancellationToken);
}
