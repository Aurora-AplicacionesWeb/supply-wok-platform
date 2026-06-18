namespace Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Commands;

public record DeleteCatalogItemCommand(int SupplierId, int CatalogItemId);
