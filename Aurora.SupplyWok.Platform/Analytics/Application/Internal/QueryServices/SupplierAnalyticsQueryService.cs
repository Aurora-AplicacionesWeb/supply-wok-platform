using Aurora.SupplyWok.Platform.Analytics.Application.QueryServices;
using Aurora.SupplyWok.Platform.Analytics.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Analytics.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Analytics.Domain.Model.ValueObjects;
using Aurora.SupplyWok.Platform.Purchasing.Interfaces.Acl;

namespace Aurora.SupplyWok.Platform.Analytics.Application.Internal.QueryServices;

public class SupplierAnalyticsQueryService(
    IPurchaseOrderContextFacade purchaseOrderFacade)
    : ISupplierAnalyticsQueryService
{
    public async Task<IEnumerable<SupplierAnalytics>> Handle(
        GetAllSupplierAnalyticsQuery query,
        CancellationToken cancellationToken)
    {
        var orders = await purchaseOrderFacade.GetAllPurchaseOrders(cancellationToken);

        var validOrders = orders.Where(o =>
            o.Status is "Delivered" or "Confirmed").ToList();

        if (validOrders.Count == 0)
            return new[] { new SupplierAnalytics(
                new List<SupplierAggregatePeriod>(),
                new List<SupplierClientDemand>()) };

        // ── SupplierAggregatePeriod: Aggregate by month ──
        var aggregate = validOrders
            .GroupBy(o => o.OrderDate[..7])
            .OrderBy(g => g.Key)
            .Select(g => new SupplierAggregatePeriod(
                g.Key,
                (int)g.Sum(o => o.Items.Sum(i => i.Quantity))))
            .ToList();

        // ── SupplierClientDemand: Per-restaurant demand with trend ──
        var clients = validOrders
            .GroupBy(o => o.RestaurantName)
            .Select(g =>
            {
                var orderedOrders = g.OrderBy(o => o.OrderDate).ToList();
                var midPoint = orderedOrders.Count / 2;
                var firstHalf = orderedOrders.Take(midPoint).SelectMany(o => o.Items).ToList();
                var secondHalf = orderedOrders.Skip(midPoint).SelectMany(o => o.Items).ToList();

                var firstAvg = firstHalf.Count != 0
                    ? (double)firstHalf.Sum(i => i.Quantity) / firstHalf.Count
                    : 0;
                var secondAvg = secondHalf.Count != 0
                    ? (double)secondHalf.Sum(i => i.Quantity) / secondHalf.Count
                    : 0;

                var totalQuantity = (int)g.SelectMany(o => o.Items).Sum(i => i.Quantity);

                string trend, summary;

                if (firstAvg == 0 && secondAvg == 0)
                {
                    trend = "stable";
                    summary = "No hay datos históricos suficientes para mostrar una proyección.";
                }
                else if (firstAvg == 0)
                {
                    trend = "upward";
                    summary = $"Nuevo cliente con pedidos recientes. Promedio de {secondAvg:F1} unidades por pedido.";
                }
                else
                {
                    var change = ((secondAvg - firstAvg) / firstAvg) * 100;
                    trend = change switch
                    {
                        > 10 => "upward",
                        < -10 => "downward",
                        _ => "stable"
                    };

                    summary = change switch
                    {
                        > 10 => $"Demanda en aumento ({change:F1}%). Últimos pedidos promedian {secondAvg:F1} unidades.",
                        < -10 => $"Demanda en descenso ({change:F1}%). Últimos pedidos promedian {secondAvg:F1} unidades.",
                        _ => $"Demanda estable ({change:F1}%). Promedio consistente de {secondAvg:F1} unidades por pedido."
                    };
                }

                return new SupplierClientDemand(0, g.Key, totalQuantity, trend, summary);
            })
            .ToList();

        var analytics = new SupplierAnalytics(aggregate, clients);
        return new[] { analytics };
    }
}
