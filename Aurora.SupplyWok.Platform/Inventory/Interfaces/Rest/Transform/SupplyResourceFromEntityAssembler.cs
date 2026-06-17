using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Inventory.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Inventory.Interfaces.Rest.Transform;

public static class SupplyResourceFromEntityAssembler
{
    public static SupplyResource ToResourceFromEntity(Supply supply)
    {
        ArgumentNullException.ThrowIfNull(supply);

        return new SupplyResource(
            supply.Id,
            supply.Name,
            supply.UnitOfMeasure.ToString(),
            supply.CurrentStock,
            supply.MinimumStockLevel,
            supply.Category);
    }
}
