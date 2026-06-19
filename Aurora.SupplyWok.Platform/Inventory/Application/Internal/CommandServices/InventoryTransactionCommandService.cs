using Aurora.SupplyWok.Platform.Inventory.Application.CommandServices;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.ValueObjects;
using Aurora.SupplyWok.Platform.Inventory.Domain.Repositories;
using Aurora.SupplyWok.Platform.Shared.Application.Model;
using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Inventory.Application.Internal.CommandServices;

public class InventoryTransactionCommandService(
    ISupplyRepository supplyRepository,
    IInventoryTransactionRepository inventoryTransactionRepository,
    IUnitOfWork unitOfWork) : IInventoryTransactionCommandService
{
    public async Task<Result<InventoryTransaction>> Handle(CreateInventoryTransactionCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var supply = await supplyRepository.GetSupplyByIdAsync(command.SupplyId, cancellationToken);
            if (supply is null)
                return Result<InventoryTransaction>.Failure(
                    InventoryError.SupplyNotFound,
                    nameof(InventoryError.SupplyNotFound));

            var transaction = new InventoryTransaction(
                command.SupplyId,
                command.Type,
                command.Amount,
                command.TransactionDate,
                command.Reason);

            switch (command.Type)
            {
                case EInventoryTransactionType.Entry:
                    transaction.AddOperation(EInventoryOperationType.Entry, command.Amount);
                    supply.IncreaseStock(command.Amount);
                    break;

                case EInventoryTransactionType.Exit:
                    transaction.AddOperation(EInventoryOperationType.Exit, command.Amount);
                    supply.DecreaseStock(command.Amount);
                    break;

                case EInventoryTransactionType.Transfer:
                    return Result<InventoryTransaction>.Failure(
                        InventoryError.TransferNotSupported,
                        nameof(InventoryError.TransferNotSupported));
            }

            supplyRepository.Update(supply);
            await inventoryTransactionRepository.AddAsync(transaction, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<InventoryTransaction>.Success(transaction);
        }
        catch (ArgumentException ex)
        {
            return Result<InventoryTransaction>.Failure(InventoryError.InvalidData, ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Result<InventoryTransaction>.Failure(InventoryError.InsufficientStock, ex.Message);
        }
        catch (OperationCanceledException)
        {
            return Result<InventoryTransaction>.Failure(
                InventoryError.OperationCancelled,
                nameof(InventoryError.OperationCancelled));
        }
        catch (DbUpdateException)
        {
            return Result<InventoryTransaction>.Failure(InventoryError.DatabaseError, nameof(InventoryError.DatabaseError));
        }
        catch (Exception ex)
        {
            return Result<InventoryTransaction>.Failure(InventoryError.InternalServerError, ex.Message);
        }
    }
}
