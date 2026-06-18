using Aurora.SupplyWok.Platform.Operations.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Operations.Domain.Model.Aggregate;

public partial class KitchenOrder
{
    public ICollection<Entities.KitchenOrderItem> Items { get; } = new List<Entities.KitchenOrderItem>();

    public KitchenOrder()
    {
        Number = string.Empty;
        TableId = 0;
        TypeService = ETypeService.TableService;
        Status = EKitchenOrderStatus.Pending;
        Observations = string.Empty;
        DateCreated = DateOnly.FromDateTime(DateTime.Now);
    }
}
