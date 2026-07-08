using System.Text.Json;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Spm.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Spm.Interfaces.Rest.Transform;

public static class SupplierSettingsResourceFromEntityAssembler
{
    public static SupplierSettingsResource ToResourceFromEntity(SupplierSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        var zones = string.IsNullOrWhiteSpace(settings.ServiceZones)
            ? []
            : JsonSerializer.Deserialize<JsonElement[]>(settings.ServiceZones) ?? [];

        var contacts = string.IsNullOrWhiteSpace(settings.Contacts)
            ? []
            : JsonSerializer.Deserialize<JsonElement[]>(settings.Contacts) ?? [];

        return new SupplierSettingsResource(
            settings.Id,
            settings.SupplierProfileId,
            settings.SupplierName,
            settings.SupportContact,
            new NotificationsInfo(settings.NotifyEmail, settings.NotifySms),
            zones,
            contacts);
    }
}
