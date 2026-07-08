using System.Text.Json;

namespace Aurora.SupplyWok.Platform.Spm.Interfaces.Rest.Resources;

public record SupplierSettingsResource(
    int Id,
    int SupplierProfileId,
    string SupplierName,
    string SupportContact,
    NotificationsInfo Notifications,
    JsonElement[] ServiceZones,
    JsonElement[] Contacts);

public record NotificationsInfo(bool Email, bool Sms);
