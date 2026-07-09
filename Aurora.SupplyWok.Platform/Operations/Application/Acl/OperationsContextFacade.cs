using System.Globalization;
using Aurora.SupplyWok.Platform.Operations.Domain.Repositories;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.ValueObjects;
using Aurora.SupplyWok.Platform.Operations.Interfaces.Acl;

namespace Aurora.SupplyWok.Platform.Operations.Application.Acl;

public class OperationsContextFacade(
    IKitchenOrderRepository kitchenOrderRepository) : IOperationsContextFacade
{
    public async Task<WeeklyConsumptionDto> GetWeeklyConsumptionAsync(CancellationToken cancellationToken = default)
    {
        var orders = await kitchenOrderRepository.ListWithItemsAsync(cancellationToken);

        var cutoff = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-7 * 6));

        var weeklyGroups = orders
            .Where(o => o.Status != EKitchenOrderStatus.Cancelled && o.DateCreated >= cutoff)
            .GroupBy(o => ISOWeek.GetWeekOfYear(o.DateCreated.ToDateTime(TimeOnly.MinValue)))
            .OrderBy(g => g.Key)
            .Select(g => new
            {
                Label = $"W{g.Key}",
                Total = g.Sum(o => o.Items.Sum(i => i.Quantity))
            })
            .ToList();

        return new WeeklyConsumptionDto(
            weeklyGroups.Select(x => x.Label).ToList(),
            weeklyGroups.Select(x => x.Total).ToList()
        );
    }
}
