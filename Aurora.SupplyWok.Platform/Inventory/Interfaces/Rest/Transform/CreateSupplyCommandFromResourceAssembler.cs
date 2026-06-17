using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.ValueObjects;
using Aurora.SupplyWok.Platform.Inventory.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Inventory.Interfaces.Rest.Transform;

public static class CreateSupplyCommandFromResourceAssembler
{
    public static CreateSupplyCommand ToCommandFromResource(CreateSupplyResource resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        return new CreateSupplyCommand(
            resource.Name,
            Enum.Parse<EUnitOfMeasure>(resource.UnitOfMeasure),
            resource.CurrentStock,
            resource.MinimumStockLevel,
            resource.Category);
    }
}
