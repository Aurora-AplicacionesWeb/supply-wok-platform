using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.ValueObjects;
using Aurora.SupplyWok.Platform.Inventory.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Inventory.Interfaces.Rest.Transform;

public static class UpdateSupplyCommandFromResourceAssembler
{
    public static UpdateSupplyCommand ToCommandFromResource(int id, UpdateSupplyResource resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        if (!Enum.TryParse<EUnitOfMeasure>(resource.UnitOfMeasure, ignoreCase: true, out var unitOfMeasure))
            throw new ArgumentException($"Invalid UnitOfMeasure value: '{resource.UnitOfMeasure}'. Valid values are: {string.Join(", ", Enum.GetNames<EUnitOfMeasure>())}");

        return new UpdateSupplyCommand(
            id,
            resource.Name,
            unitOfMeasure,
            resource.MinimumStockLevel,
            resource.Category);
    }
}
