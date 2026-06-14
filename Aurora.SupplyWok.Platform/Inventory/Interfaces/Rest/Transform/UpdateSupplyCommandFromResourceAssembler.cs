using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.ValueObjects;
using Aurora.SupplyWok.Platform.Inventory.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Inventory.Interfaces.Rest.Transform;

public static class UpdateSupplyCommandFromResourceAssembler
{
    public static UpdateSupplyCommand ToCommandFromResource(int id, UpdateSupplyResource resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        return new UpdateSupplyCommand(
            id,
            resource.Name,
            Enum.Parse<EUnitOfMeasure>(resource.UnitOfMeasure),
            resource.MinimumStockLevel,
            resource.Category);
    }
}
