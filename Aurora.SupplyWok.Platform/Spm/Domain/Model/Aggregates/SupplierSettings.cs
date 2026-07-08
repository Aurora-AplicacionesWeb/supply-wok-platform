using Aurora.SupplyWok.Platform.Spm.Domain.Model.Commands;

namespace Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;

public partial class SupplierSettings
{
    public SupplierSettings(
        int supplierProfileId,
        string supplierName,
        string supportContact,
        bool notifyEmail,
        bool notifySms,
        string serviceZones,
        string contacts) : this()
    {
        SetSupplierProfileId(supplierProfileId);
        UpdateCore(supplierName, supportContact, notifyEmail, notifySms, serviceZones, contacts);
    }

    public SupplierSettings(CreateSupplierSettingsCommand command) : this(
        command.SupplierProfileId,
        command.SupplierName,
        command.SupportContact,
        command.NotifyEmail,
        command.NotifySms,
        command.ServiceZones,
        command.Contacts)
    {
    }

    public int Id { get; private set; }

    public int SupplierProfileId { get; private set; }

    public string SupplierName { get; private set; }

    public string SupportContact { get; private set; }

    public bool NotifyEmail { get; private set; }

    public bool NotifySms { get; private set; }

    public string ServiceZones { get; private set; }

    public string Contacts { get; private set; }

    public void Update(string supplierName, string supportContact, bool notifyEmail, bool notifySms, string serviceZones, string contacts)
    {
        UpdateCore(supplierName, supportContact, notifyEmail, notifySms, serviceZones, contacts);
    }

    private void SetSupplierProfileId(int supplierProfileId)
    {
        if (supplierProfileId <= 0)
            throw new ArgumentException("Supplier profile id must be greater than zero.", nameof(supplierProfileId));

        SupplierProfileId = supplierProfileId;
    }

    private void UpdateCore(string supplierName, string supportContact, bool notifyEmail, bool notifySms, string serviceZones, string contacts)
    {
        if (string.IsNullOrWhiteSpace(supplierName))
            throw new ArgumentException("Supplier name cannot be empty.", nameof(supplierName));
        if (string.IsNullOrWhiteSpace(supportContact))
            throw new ArgumentException("Support contact cannot be empty.", nameof(supportContact));

        SupplierName = supplierName.Trim();
        SupportContact = supportContact.Trim();
        NotifyEmail = notifyEmail;
        NotifySms = notifySms;
        ServiceZones = serviceZones;
        Contacts = contacts;
    }
}
