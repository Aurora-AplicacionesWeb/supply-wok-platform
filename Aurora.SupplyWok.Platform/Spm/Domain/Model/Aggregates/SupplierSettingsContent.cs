namespace Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;

public partial class SupplierSettings
{
    public SupplierSettings()
    {
        SupplierName = string.Empty;
        SupportContact = string.Empty;
        ServiceZones = "[]";
        Contacts = "[]";
    }
}
