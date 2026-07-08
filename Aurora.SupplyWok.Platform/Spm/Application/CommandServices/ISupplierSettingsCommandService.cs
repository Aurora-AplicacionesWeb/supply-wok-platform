using Aurora.SupplyWok.Platform.Shared.Application.Model;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Commands;

namespace Aurora.SupplyWok.Platform.Spm.Application.CommandServices;

public interface ISupplierSettingsCommandService
{
    Task<Result<SupplierSettings>> Handle(CreateSupplierSettingsCommand command, CancellationToken cancellationToken);
    Task<Result<SupplierSettings>> Handle(UpdateSupplierSettingsCommand command, CancellationToken cancellationToken);
}
