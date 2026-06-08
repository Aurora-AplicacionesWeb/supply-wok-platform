using Aurora.SupplyWok.Platform.Operations.Application.CommandServices;
using Aurora.SupplyWok.Platform.Operations.Domain.Model;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Operations.Domain.Repositories;
using Aurora.SupplyWok.Platform.Shared.Resources.Errors;
using Aurora.SupplyWok.Platform.Shared.Application.Model;
using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Aurora.SupplyWok.Platform.Operations.Application.Internal.CommandServices;

public class TableCommandService(
    ITableRepository tableRepository, 
    IUnitOfWork unitOfWork, 
    IStringLocalizer<ErrorMessages> localizer) 
    : ITableCommandService
{
    private readonly IStringLocalizer<ErrorMessages> _localizer = localizer;
    
    public async Task<Result<Table>> Handle(CreateTableCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var table = new Table(command.Number, command.Capacity, command.Location, command.State, command.Active);
            await tableRepository.AddAsync(table, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Table>.Success(table);
        }
        catch (OperationCanceledException)
        {
            return Result<Table>.Failure(OperationsError.OperationCancelled, _localizer[nameof(OperationsError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Table>.Failure(OperationsError.DatabaseError, 
                _localizer[nameof(OperationsError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Table>.Failure(OperationsError.InternalServerError, 
                _localizer[nameof(OperationsError.InternalServerError)]);
        }
    }

    public async Task<Result<Table>> Handle(UpdateTableCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var table = await tableRepository.GetTableByIdAsync(command.Id, cancellationToken);
            if (table == null)
            {
                return Result<Table>.Failure(OperationsError.TableNotFound,
                    string.Format(_localizer[nameof(OperationsError.TableNotFound)], command.Id));
            }

            table.Update(command.Number, command.Capacity, command.Location, command.State, command.Active);

            tableRepository.Update(table);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Table>.Success(table);
        }
        catch (ArgumentException ex)
        {
            return Result<Table>.Failure(OperationsError.InternalServerError, ex.Message);
        }
        catch (OperationCanceledException)
        {
            return Result<Table>.Failure(OperationsError.OperationCancelled,
                _localizer[nameof(OperationsError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Table>.Failure(OperationsError.DatabaseError, 
                _localizer[nameof(OperationsError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Table>.Failure(OperationsError.InternalServerError, 
                _localizer[nameof(OperationsError.InternalServerError)]);
        }
    }

    public async Task<Result<bool>> Handle(DeleteTableCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var table = await tableRepository.GetTableByIdAsync(command.Id, cancellationToken);
            if (table == null)
            {
                return Result<bool>.Failure(OperationsError.TableNotFound,
                    string.Format(_localizer[nameof(OperationsError.TableNotFound)], command.Id));
            }

            tableRepository.Remove(table);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<bool>.Success(true);
        }
        catch (OperationCanceledException)
        {
            return Result<bool>.Failure(OperationsError.OperationCancelled,
                _localizer[nameof(OperationsError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<bool>.Failure(OperationsError.DatabaseError,
                _localizer[nameof(OperationsError.DatabaseError)]);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(OperationsError.InternalServerError,
                ex.Message);
        }
    }
}