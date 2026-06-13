using Aurora.SupplyWok.Platform.Inventory.Application.CommandServices;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Inventory.Domain.Repositories;
using Aurora.SupplyWok.Platform.Shared.Application.Model;
using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Inventory.Application.Internal.CommandServices;

public class StockMovementCommandService(
    ISupplyRepository supplyRepository,
    IUnitOfWork unitOfWork) : IStockMovementCommandService
{
    public async Task<Result<StockMovement>> Handle(CreateStockMovementCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var supply = await supplyRepository.GetSupplyByIdAsync(command.SupplyId, cancellationToken);
            if (supply is null)
                return Result<StockMovement>.Failure(
                    InventoryError.SupplyNotFound,
                    nameof(InventoryError.SupplyNotFound));

            supply.ApplyMovement(command.Type, command.Amount);
            var movement = new StockMovement(
                command.SupplyId,
                command.Type,
                command.Amount,
                command.Date,
                command.Reason);

            supplyRepository.Update(supply);
            await supplyRepository.AddStockMovementAsync(movement, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<StockMovement>.Success(movement);
        }
        catch (ArgumentException ex)
        {
            return Result<StockMovement>.Failure(InventoryError.InvalidData, ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Result<StockMovement>.Failure(InventoryError.InsufficientStock, ex.Message);
        }
        catch (OperationCanceledException)
        {
            return Result<StockMovement>.Failure(
                InventoryError.OperationCancelled,
                nameof(InventoryError.OperationCancelled));
        }
        catch (DbUpdateException)
        {
            return Result<StockMovement>.Failure(InventoryError.DatabaseError, nameof(InventoryError.DatabaseError));
        }
        catch (Exception ex)
        {
            return Result<StockMovement>.Failure(InventoryError.InternalServerError, ex.Message);
        }
    }
}
