using Aurora.SupplyWok.Platform.Operations.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Operations.Domain.Model.Commands;

public record UpdateKitchenOrderStatusCommand(int Id, EKitchenOrderStatus Status, string Observations);
