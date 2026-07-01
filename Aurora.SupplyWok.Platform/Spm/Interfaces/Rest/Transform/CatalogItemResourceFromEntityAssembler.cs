using Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.ValueObjects;
using Aurora.SupplyWok.Platform.Spm.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Spm.Interfaces.Rest.Transform;

public static class CatalogItemResourceFromEntityAssembler
{
    public static CatalogItemResource ToResourceFromEntity(CatalogItem catalogItem)
    {
        ArgumentNullException.ThrowIfNull(catalogItem);

        return new CatalogItemResource(
            catalogItem.Id,
            catalogItem.Name,
            catalogItem.Category,
            catalogItem.Price,
            ToUnitString(catalogItem.Unit),
            catalogItem.DeliveryConditions);
    }

    private static string ToUnitString(ECatalogUnit unit)
    {
        return unit switch
        {
            ECatalogUnit.Kg => "kg",
            ECatalogUnit.Ltr => "ltr",
            ECatalogUnit.Box => "box",
            _ => unit.ToString().ToLowerInvariant()
        };
    }
}
