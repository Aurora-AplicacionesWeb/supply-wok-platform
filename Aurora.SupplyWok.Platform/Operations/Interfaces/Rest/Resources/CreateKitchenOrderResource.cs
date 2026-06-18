using Aurora.SupplyWok.Platform.Operations.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Operations.Interfaces.Rest.Resources;

public record CreateKitchenOrderResource(
    string Number,
    int TableId,
    ETypeService TypeService,
    string Observations,
    DateOnly DateCreated
);
