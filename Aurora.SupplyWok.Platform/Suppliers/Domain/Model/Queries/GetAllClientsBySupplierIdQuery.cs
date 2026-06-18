namespace Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Queries;

/// <summary>
///     Query to retrieve all clients linked to a supplier.
/// </summary>
/// <param name="SupplierId">The supplier identifier.</param>
public record GetAllClientsBySupplierIdQuery(int SupplierId);
