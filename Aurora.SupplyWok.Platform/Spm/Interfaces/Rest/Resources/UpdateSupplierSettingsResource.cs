using System.Text.Json;

namespace Aurora.SupplyWok.Platform.Spm.Interfaces.Rest.Resources;

public record UpdateSupplierSettingsResource(
    string SupplierName,
    string SupportContact,
    NotificationsInfo Notifications,
    JsonElement[] ServiceZones,
    JsonElement[] Contacts);
