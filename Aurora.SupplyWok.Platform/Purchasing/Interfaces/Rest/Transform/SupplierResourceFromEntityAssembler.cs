using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Purchasing.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Purchasing.Interfaces.Rest.Transform;

public static class SupplierResourceFromEntityAssembler
{
    public static SupplierResource ToResourceFromEntity(Supplier supplier)
    {
        ArgumentNullException.ThrowIfNull(supplier);

        return new SupplierResource(supplier.Id, supplier.Name, supplier.ContactName, supplier.Email, supplier.Phone,
            supplier.Category, supplier.LinkedDate, supplier.Sla, supplier.ResponseTime);
    }
}
