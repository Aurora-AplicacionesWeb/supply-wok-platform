using Aurora.SupplyWok.Platform.Operations.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Operations.Interfaces.Rest.Resources;

public record KitchenOrderResource(
    int Id,
    string Number,
    int TableId,
    ETypeService TypeService,
    EKitchenOrderStatus Status,
    string Observations,
    DateOnly DateCreated,
    DateTime? HourReady,
    DateTime? HourDelivered,
    int PreparationTime,
    double TotalPrice,
    List<KitchenOrderItemResource> Items
);
