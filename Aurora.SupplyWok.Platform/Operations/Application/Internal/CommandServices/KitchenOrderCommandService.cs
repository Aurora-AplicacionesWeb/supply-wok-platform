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

public class KitchenOrderCommandService(
    IKitchenOrderRepository kitchenOrderRepository,
    IDishRepository dishRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer)
    : IKitchenOrderCommandService
{
    public async Task<Result<KitchenOrder>> Handle(CreateKitchenOrderCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var kitchenOrder = new KitchenOrder(command);
            await kitchenOrderRepository.AddAsync(kitchenOrder, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<KitchenOrder>.Success(kitchenOrder);
        }
        catch (OperationCanceledException)
        {
            return Result<KitchenOrder>.Failure(OperationsError.OperationCancelled,
                localizer[nameof(OperationsError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<KitchenOrder>.Failure(OperationsError.DatabaseError,
                localizer[nameof(OperationsError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<KitchenOrder>.Failure(OperationsError.InternalServerError,
                localizer[nameof(OperationsError.InternalServerError)]);
        }
    }

    public async Task<Result<KitchenOrder>> Handle(UpdateKitchenOrderCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var kitchenOrder = await kitchenOrderRepository.FindByIdAsync(command.Id, cancellationToken);
            if (kitchenOrder == null)
            {
                return Result<KitchenOrder>.Failure(OperationsError.KitchenOrderNotFound,
                    string.Format(localizer[nameof(OperationsError.KitchenOrderNotFound)], command.Id));
            }

            kitchenOrder.Update(command.Number, command.TableId, command.TypeService, command.Observations);
            kitchenOrderRepository.Update(kitchenOrder);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<KitchenOrder>.Success(kitchenOrder);
        }
        catch (ArgumentException ex)
        {
            return Result<KitchenOrder>.Failure(OperationsError.InternalServerError, ex.Message);
        }
        catch (OperationCanceledException)
        {
            return Result<KitchenOrder>.Failure(OperationsError.OperationCancelled,
                localizer[nameof(OperationsError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<KitchenOrder>.Failure(OperationsError.DatabaseError,
                localizer[nameof(OperationsError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<KitchenOrder>.Failure(OperationsError.InternalServerError,
                localizer[nameof(OperationsError.InternalServerError)]);
        }
    }

    public async Task<Result<bool>> Handle(DeleteKitchenOrderCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var kitchenOrder = await kitchenOrderRepository.FindByIdAsync(command.Id, cancellationToken);
            if (kitchenOrder == null)
            {
                return Result<bool>.Failure(OperationsError.KitchenOrderNotFound,
                    string.Format(localizer[nameof(OperationsError.KitchenOrderNotFound)], command.Id));
            }

            kitchenOrderRepository.Remove(kitchenOrder);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<bool>.Success(true);
        }
        catch (OperationCanceledException)
        {
            return Result<bool>.Failure(OperationsError.OperationCancelled,
                localizer[nameof(OperationsError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<bool>.Failure(OperationsError.DatabaseError,
                localizer[nameof(OperationsError.DatabaseError)]);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(OperationsError.InternalServerError, ex.Message);
        }
    }

    public async Task<Result<KitchenOrder>> Handle(UpdateKitchenOrderStatusCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var kitchenOrder = await kitchenOrderRepository.FindByIdAsync(command.Id, cancellationToken);
            if (kitchenOrder == null)
            {
                return Result<KitchenOrder>.Failure(OperationsError.KitchenOrderNotFound,
                    string.Format(localizer[nameof(OperationsError.KitchenOrderNotFound)], command.Id));
            }

            kitchenOrder.UpdateStatus(command.Status);
            kitchenOrderRepository.Update(kitchenOrder);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<KitchenOrder>.Success(kitchenOrder);
        }
        catch (InvalidOperationException ex)
        {
            return Result<KitchenOrder>.Failure(OperationsError.InternalServerError, ex.Message);
        }
        catch (OperationCanceledException)
        {
            return Result<KitchenOrder>.Failure(OperationsError.OperationCancelled,
                localizer[nameof(OperationsError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<KitchenOrder>.Failure(OperationsError.DatabaseError,
                localizer[nameof(OperationsError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<KitchenOrder>.Failure(OperationsError.InternalServerError,
                localizer[nameof(OperationsError.InternalServerError)]);
        }
    }

    public async Task<Result<KitchenOrder>> Handle(AddDishToKitchenOrderCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var kitchenOrder = await kitchenOrderRepository.FindByIdWithItemsAsync(command.KitchenOrderId, cancellationToken);
            if (kitchenOrder == null)
            {
                return Result<KitchenOrder>.Failure(OperationsError.KitchenOrderNotFound,
                    string.Format(localizer[nameof(OperationsError.KitchenOrderNotFound)], command.KitchenOrderId));
            }

            var dish = await dishRepository.FindByIdAsync(command.DishId, cancellationToken);
            if (dish == null)
            {
                return Result<KitchenOrder>.Failure(OperationsError.DishNotFound,
                    string.Format(localizer[nameof(OperationsError.DishNotFound)], command.DishId));
            }

            kitchenOrder.AddDish(dish.Id, dish.Name, command.Quantity, dish.Price);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<KitchenOrder>.Success(kitchenOrder);
        }
        catch (OperationCanceledException)
        {
            return Result<KitchenOrder>.Failure(OperationsError.OperationCancelled,
                localizer[nameof(OperationsError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<KitchenOrder>.Failure(OperationsError.DatabaseError,
                localizer[nameof(OperationsError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<KitchenOrder>.Failure(OperationsError.InternalServerError,
                localizer[nameof(OperationsError.InternalServerError)]);
        }
    }
}
