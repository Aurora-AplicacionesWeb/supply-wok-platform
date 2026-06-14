namespace Aurora.SupplyWok.Platform.Inventory.Interfaces.Acl;

public interface IInventoryContextFacade
{
    Task<int> GetTotalSupplyStockAsync(CancellationToken cancellationToken);
}
