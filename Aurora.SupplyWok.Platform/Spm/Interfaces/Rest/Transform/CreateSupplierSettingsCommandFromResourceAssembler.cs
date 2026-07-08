using System.Text.Json;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Spm.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Spm.Interfaces.Rest.Transform;

public static class CreateSupplierSettingsCommandFromResourceAssembler
{
    public static CreateSupplierSettingsCommand ToCommandFromResource(int supplierProfileId, UpdateSupplierSettingsResource resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        var zones = JsonSerializer.Serialize(resource.ServiceZones);
        var contacts = JsonSerializer.Serialize(resource.Contacts);

        return new CreateSupplierSettingsCommand(
            supplierProfileId,
            resource.SupplierName,
            resource.SupportContact,
            resource.Notifications.Email,
            resource.Notifications.Sms,
            zones,
            contacts);
    }
}
