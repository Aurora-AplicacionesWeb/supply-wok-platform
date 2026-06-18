using Aurora.SupplyWok.Platform.Operations.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Operations.Domain.Model.Commands;

public record UpdateKitchenOrderCommand(
    int Id,
    string Number,
    int TableId,
    ETypeService TypeService,
    string Observations,
    DateOnly DateCreated
);
