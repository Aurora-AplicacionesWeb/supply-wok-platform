using Aurora.SupplyWok.Platform.Shared.Application.Model;
using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;
using Aurora.SupplyWok.Platform.Profiles.Interfaces.Acl;
using Aurora.SupplyWok.Platform.Spm.Application.CommandServices;
using Aurora.SupplyWok.Platform.Spm.Domain.Model;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Spm.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Spm.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Spm.Application.Internal.CommandServices;

public class SupplierSettingsCommandService(
    ISupplierSettingsRepository supplierSettingsRepository,
    IProfilesContextFacade profilesContextFacade,
    IUnitOfWork unitOfWork) : ISupplierSettingsCommandService
{
    public async Task<Result<SupplierSettings>> Handle(CreateSupplierSettingsCommand command, CancellationToken cancellationToken)
    {
        try
        {
            if (await profilesContextFacade.GetSupplierProfileById(command.SupplierProfileId, cancellationToken) is null)
                return Result<SupplierSettings>.Failure(SuppliersError.SupplierNotFound, nameof(SuppliersError.SupplierNotFound));

            var existing = await supplierSettingsRepository.FindBySupplierProfileIdAsync(command.SupplierProfileId, cancellationToken);
            if (existing is not null)
                return Result<SupplierSettings>.Failure(SuppliersError.InvalidData, "Supplier settings already exist.");

            var settings = new SupplierSettings(command);
            await supplierSettingsRepository.AddAsync(settings, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<SupplierSettings>.Success(settings);
        }
        catch (ArgumentException ex)
        {
            return Result<SupplierSettings>.Failure(SuppliersError.InvalidData, ex.Message);
        }
        catch (OperationCanceledException)
        {
            return Result<SupplierSettings>.Failure(SuppliersError.OperationCancelled, nameof(SuppliersError.OperationCancelled));
        }
        catch (DbUpdateException)
        {
            return Result<SupplierSettings>.Failure(SuppliersError.DatabaseError, nameof(SuppliersError.DatabaseError));
        }
        catch (Exception ex)
        {
            return Result<SupplierSettings>.Failure(SuppliersError.InternalServerError, ex.Message);
        }
    }

    public async Task<Result<SupplierSettings>> Handle(UpdateSupplierSettingsCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var settings = await supplierSettingsRepository.FindBySupplierProfileIdAsync(command.SupplierProfileId, cancellationToken);
            if (settings is null)
                return Result<SupplierSettings>.Failure(SuppliersError.SupplierSettingsNotFound, nameof(SuppliersError.SupplierSettingsNotFound));

            settings.Update(command.SupplierName, command.SupportContact, command.NotifyEmail, command.NotifySms, command.ServiceZones, command.Contacts);
            supplierSettingsRepository.Update(settings);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<SupplierSettings>.Success(settings);
        }
        catch (ArgumentException ex)
        {
            return Result<SupplierSettings>.Failure(SuppliersError.InvalidData, ex.Message);
        }
        catch (OperationCanceledException)
        {
            return Result<SupplierSettings>.Failure(SuppliersError.OperationCancelled, nameof(SuppliersError.OperationCancelled));
        }
        catch (DbUpdateException)
        {
            return Result<SupplierSettings>.Failure(SuppliersError.DatabaseError, nameof(SuppliersError.DatabaseError));
        }
        catch (Exception ex)
        {
            return Result<SupplierSettings>.Failure(SuppliersError.InternalServerError, ex.Message);
        }
    }
}
