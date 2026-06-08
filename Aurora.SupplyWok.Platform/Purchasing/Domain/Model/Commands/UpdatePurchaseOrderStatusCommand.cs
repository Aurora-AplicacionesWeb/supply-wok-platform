namespace Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Commands;

public record UpdatePurchaseOrderStatusCommand(int Id, string Status);
