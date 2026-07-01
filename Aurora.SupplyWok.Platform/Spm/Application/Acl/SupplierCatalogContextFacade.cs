using Aurora.SupplyWok.Platform.Spm.Application.QueryServices;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Spm.Interfaces.Acl;

namespace Aurora.SupplyWok.Platform.Spm.Application.Acl;

/// <summary>
/// Supplier-facing facade that exposes catalog data through the ACL.
/// </summary>
public class SupplierCatalogContextFacade(ICatalogItemQueryService catalogItemQueryService) : ISupplierCatalogContextFacade
{
    /// <inheritdoc />
    public async Task<IEnumerable<CatalogItemAclResource>> GetCatalogItemsBySupplierId(int supplierId, CancellationToken cancellationToken)
    {
        var catalogItems = await catalogItemQueryService.Handle(new GetAllCatalogItemsBySupplierIdQuery(supplierId), cancellationToken);
        return catalogItems.Select(ToAclResource);
    }

    /// <inheritdoc />
    public async Task<CatalogItemAclResource?> GetCatalogItemById(int supplierId, int catalogItemId, CancellationToken cancellationToken)
    {
        var catalogItem = await catalogItemQueryService.Handle(new GetCatalogItemByIdQuery(supplierId, catalogItemId), cancellationToken);
        return catalogItem is null ? null : ToAclResource(catalogItem);
    }

    private static CatalogItemAclResource ToAclResource(Domain.Model.Aggregates.CatalogItem catalogItem)
    {
        return new CatalogItemAclResource(
            catalogItem.Id,
            catalogItem.SupplierId,
            catalogItem.Name,
            catalogItem.Category,
            catalogItem.Price,
            catalogItem.Unit.ToString(),
            catalogItem.DeliveryConditions);
    }
}
