namespace Aurora.SupplyWok.Platform.Analytics.Domain.Model.Queries;

public record GetAllSupplierAnalyticsQuery(int? ClientId = null, int? ProductId = null);
