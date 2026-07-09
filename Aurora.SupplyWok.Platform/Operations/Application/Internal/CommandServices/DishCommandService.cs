using Aurora.SupplyWok.Platform.Operations.Application.CommandServices;
using Aurora.SupplyWok.Platform.Operations.Domain.Model;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Operations.Domain.Repositories;
using Aurora.SupplyWok.Platform.Shared.Application.Model;
using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;
using Aurora.SupplyWok.Platform.Shared.Resources.Errors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Aurora.SupplyWok.Platform.Operations.Application.Internal.CommandServices;

public class DishCommandService(
    IDishRepository dishRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer) : IDishCommandService
{
    private readonly IStringLocalizer<ErrorMessages> _localizer = localizer;

    public async Task<Result<Dish>> Handle(CreateDishCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var dish = new Dish(
                command.Code,
                command.Name,
                command.Quantity,
                command.Description,
                command.Price,
                command.Active,
                command.Outstanding,
                command.DishCategoryId);

            await dishRepository.AddAsync(dish, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Dish>.Success(dish);
        }
        catch (OperationCanceledException)
        {
            return Result<Dish>.Failure(
                OperationsError.OperationCancelled,
                _localizer[nameof(OperationsError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Dish>.Failure(
                OperationsError.DatabaseError,
                _localizer[nameof(OperationsError.DatabaseError)]);
        }
        catch (Exception ex)
        {
            return Result<Dish>.Failure(
                OperationsError.InternalServerError,
                ex.Message);
        }
    }
}
