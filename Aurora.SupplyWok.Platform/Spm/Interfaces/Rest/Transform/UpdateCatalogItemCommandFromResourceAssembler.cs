using Aurora.SupplyWok.Platform.Spm.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.ValueObjects;
using Aurora.SupplyWok.Platform.Spm.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Spm.Interfaces.Rest.Transform;

public static class UpdateCatalogItemCommandFromResourceAssembler
{
    public static UpdateCatalogItemCommand ToCommandFromResource(int supplierId, int catalogItemId, UpdateCatalogItemResource resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        return new UpdateCatalogItemCommand(
            supplierId,
            catalogItemId,
            resource.Name,
            resource.Category,
            resource.Price,
            ParseUnit(resource.Unit),
            resource.DeliveryConditions);
    }

    private static ECatalogUnit ParseUnit(string unit)
    {
        if (string.Equals(unit, "kg", StringComparison.OrdinalIgnoreCase)) return ECatalogUnit.Kg;
        if (string.Equals(unit, "ltr", StringComparison.OrdinalIgnoreCase)) return ECatalogUnit.Ltr;
        if (string.Equals(unit, "box", StringComparison.OrdinalIgnoreCase)) return ECatalogUnit.Box;

        throw new ArgumentException("Unsupported catalog unit.", nameof(unit));
    }
}
