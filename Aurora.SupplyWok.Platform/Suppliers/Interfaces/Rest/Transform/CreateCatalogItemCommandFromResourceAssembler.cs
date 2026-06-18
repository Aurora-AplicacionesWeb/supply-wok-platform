using Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Model.ValueObjects;
using Aurora.SupplyWok.Platform.Suppliers.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Suppliers.Interfaces.Rest.Transform;

public static class CreateCatalogItemCommandFromResourceAssembler
{
    public static CreateCatalogItemCommand ToCommandFromResource(int supplierId, CreateCatalogItemResource resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        return new CreateCatalogItemCommand(
            supplierId,
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
