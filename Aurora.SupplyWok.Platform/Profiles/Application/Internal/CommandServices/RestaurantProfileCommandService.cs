using Aurora.SupplyWok.Platform.Profiles.Application.CommandServices;
using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Profiles.Domain.Model;
using Aurora.SupplyWok.Platform.Profiles.Domain.Repositories;
using Aurora.SupplyWok.Platform.Shared.Application.Model;
using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Profiles.Application.Internal.CommandServices;

/// <summary>
///     Restaurant profile command service implementation
/// </summary>
/// <remarks>
///     Implements <see cref="IRestaurantProfileCommandService" /> to handle restaurant profile commands
/// </remarks>
public class RestaurantProfileCommandService(
    IRestaurantProfileRepository restaurantProfileRepository,
    IUnitOfWork unitOfWork) : IRestaurantProfileCommandService
{
    /// <summary>
    ///     Handle the creation of a new restaurant profile
    /// </summary>
    /// <param name="command">
    ///     The <see cref="CreateRestaurantProfileCommand" /> with the data of the restaurant profile to create
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A <see cref="Result{T}" /> with the created <see cref="RestaurantProfile" /> if successful,
    ///     or the corresponding <see cref="ProfilesError" /> otherwise
    /// </returns>
    public async Task<Result<RestaurantProfile>> Handle(CreateRestaurantProfileCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var restaurantProfile = new RestaurantProfile(command);
            await restaurantProfileRepository.AddAsync(restaurantProfile, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<RestaurantProfile>.Success(restaurantProfile);
        }
        catch (ArgumentException ex)
        {
            return Result<RestaurantProfile>.Failure(ProfilesError.InvalidData, ex.Message);
        }
        catch (OperationCanceledException)
        {
            return Result<RestaurantProfile>.Failure(ProfilesError.OperationCancelled, nameof(ProfilesError.OperationCancelled));
        }
        catch (DbUpdateException)
        {
            return Result<RestaurantProfile>.Failure(ProfilesError.DatabaseError, nameof(ProfilesError.DatabaseError));
        }
        catch (Exception ex)
        {
            return Result<RestaurantProfile>.Failure(ProfilesError.InternalServerError, ex.Message);
        }
    }

    /// <inheritdoc />
    public async Task<Result<RestaurantProfile>> Handle(UpdateRestaurantProfileCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var restaurantProfile = await restaurantProfileRepository.FindByIdAsync(command.Id, cancellationToken);
            if (restaurantProfile is null)
                return Result<RestaurantProfile>.Failure(ProfilesError.RestaurantProfileNotFound, "Restaurant profile not found.");

            restaurantProfile.UpdateBusinessName(command.BusinessName);
            restaurantProfile.UpdateContactInfo(command.ContactName, command.ContactEmail);
            restaurantProfile.UpdateAddress(command.Address);

            restaurantProfileRepository.Update(restaurantProfile);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<RestaurantProfile>.Success(restaurantProfile);
        }
        catch (ArgumentException ex)
        {
            return Result<RestaurantProfile>.Failure(ProfilesError.InvalidData, ex.Message);
        }
        catch (OperationCanceledException)
        {
            return Result<RestaurantProfile>.Failure(ProfilesError.OperationCancelled, nameof(ProfilesError.OperationCancelled));
        }
        catch (DbUpdateException)
        {
            return Result<RestaurantProfile>.Failure(ProfilesError.DatabaseError, nameof(ProfilesError.DatabaseError));
        }
        catch (Exception ex)
        {
            return Result<RestaurantProfile>.Failure(ProfilesError.InternalServerError, ex.Message);
        }
    }
}
