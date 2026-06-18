using Aurora.SupplyWok.Platform.Shared.Application.Model;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Commands;

namespace Aurora.SupplyWok.Platform.Suppliers.Application.CommandServices;

public interface ICatalogItemCommandService
{
    Task<Result<CatalogItem>> Handle(CreateCatalogItemCommand command, CancellationToken cancellationToken);
    Task<Result<CatalogItem>> Handle(UpdateCatalogItemCommand command, CancellationToken cancellationToken);
    Task<Result<bool>> Handle(DeleteCatalogItemCommand command, CancellationToken cancellationToken);
}
