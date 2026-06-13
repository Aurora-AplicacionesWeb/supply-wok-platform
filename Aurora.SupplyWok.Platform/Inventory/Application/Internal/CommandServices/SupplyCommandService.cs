using Aurora.SupplyWok.Platform.Inventory.Application.CommandServices;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Inventory.Domain.Repositories;
using Aurora.SupplyWok.Platform.Shared.Application.Model;
using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Inventory.Application.Internal.CommandServices;

public class SupplyCommandService(ISupplyRepository supplyRepository, IUnitOfWork unitOfWork) : ISupplyCommandService
{
    public async Task<Result<Supply>> Handle(CreateSupplyCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var supply = new Supply(command);
            await supplyRepository.AddAsync(supply, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Supply>.Success(supply);
        }
        catch (ArgumentException ex)
        {
            return Result<Supply>.Failure(InventoryError.InvalidData, ex.Message);
        }
        catch (OperationCanceledException)
        {
            return Result<Supply>.Failure(InventoryError.OperationCancelled, nameof(InventoryError.OperationCancelled));
        }
        catch (DbUpdateException)
        {
            return Result<Supply>.Failure(InventoryError.DatabaseError, nameof(InventoryError.DatabaseError));
        }
        catch (Exception ex)
        {
            return Result<Supply>.Failure(InventoryError.InternalServerError, ex.Message);
        }
    }

    public async Task<Result<Supply>> Handle(UpdateSupplyCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var supply = await supplyRepository.GetSupplyByIdAsync(command.Id, cancellationToken);
            if (supply is null)
                return Result<Supply>.Failure(InventoryError.SupplyNotFound, nameof(InventoryError.SupplyNotFound));

            supply.Update(command.Name, command.UnitOfMeasure, command.MinimumStockLevel, command.category);

            supplyRepository.Update(supply);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Supply>.Success(supply);
        }
        catch (ArgumentException ex)
        {
            return Result<Supply>.Failure(InventoryError.InvalidData, ex.Message);
        }
        catch (OperationCanceledException)
        {
            return Result<Supply>.Failure(InventoryError.OperationCancelled, nameof(InventoryError.OperationCancelled));
        }
        catch (DbUpdateException)
        {
            return Result<Supply>.Failure(InventoryError.DatabaseError, nameof(InventoryError.DatabaseError));
        }
        catch (Exception ex)
        {
            return Result<Supply>.Failure(InventoryError.InternalServerError, ex.Message);
        }
    }

    public async Task<Result<bool>> Handle(DeleteSupplyCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var supply = await supplyRepository.GetSupplyByIdAsync(command.Id, cancellationToken);
            if (supply is null)
                return Result<bool>.Failure(InventoryError.SupplyNotFound, nameof(InventoryError.SupplyNotFound));

            supplyRepository.Remove(supply);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<bool>.Success(true);
        }
        catch (OperationCanceledException)
        {
            return Result<bool>.Failure(InventoryError.OperationCancelled, nameof(InventoryError.OperationCancelled));
        }
        catch (DbUpdateException)
        {
            return Result<bool>.Failure(InventoryError.DatabaseError, nameof(InventoryError.DatabaseError));
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(InventoryError.InternalServerError, ex.Message);
        }
    }
}
