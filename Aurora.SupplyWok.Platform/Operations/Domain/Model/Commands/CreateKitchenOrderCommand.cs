using Aurora.SupplyWok.Platform.Operations.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Operations.Domain.Model.Commands;

public record CreateKitchenOrderCommand(
    string Number,
    int TableId,
    ETypeService TypeService,
    string Observations,
    DateOnly DateCreated
);
