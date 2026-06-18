using Aurora.SupplyWok.Platform.Inventory.Application.QueryServices;

namespace Aurora.SupplyWok.Platform.Inventory.Domain.Model.Queries;

public record GetTotalSupplyStockQuery
{
    /// <summary>
    /// Handles the total supply stock query.
    /// </summary>
    /// <param name="supplyQueryServices">The supply query services.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The total current stock across supplies.</returns>
    public async Task<int> Handle(ISupplyQueryServices supplyQueryServices, CancellationToken cancellationToken)
    {
        return await supplyQueryServices.Handle(this, cancellationToken);
    }
}
