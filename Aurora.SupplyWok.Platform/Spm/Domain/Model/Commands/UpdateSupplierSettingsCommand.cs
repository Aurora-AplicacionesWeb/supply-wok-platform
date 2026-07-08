namespace Aurora.SupplyWok.Platform.Spm.Domain.Model.Commands;

public record UpdateSupplierSettingsCommand(
    int SupplierProfileId,
    string SupplierName,
    string SupportContact,
    bool NotifyEmail,
    bool NotifySms,
    string ServiceZones,
    string Contacts);
