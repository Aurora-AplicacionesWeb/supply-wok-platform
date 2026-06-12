using Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Suppliers.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Suppliers.Interfaces.Rest.Transform;

public static class SupplierResourceFromEntityAssembler
{
    public static SupplierResource ToResourceFromEntity(Supplier supplier)
    {
        ArgumentNullException.ThrowIfNull(supplier);

        return new SupplierResource(supplier.Id, supplier.Uuid, supplier.Name, supplier.ContactName, supplier.Email, supplier.Phone,
            supplier.Category, supplier.LinkedDate, supplier.Sla, supplier.ResponseTime);
    }
}
