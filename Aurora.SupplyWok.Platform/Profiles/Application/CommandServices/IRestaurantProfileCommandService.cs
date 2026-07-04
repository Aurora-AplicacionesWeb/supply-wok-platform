using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Shared.Application.Model;

namespace Aurora.SupplyWok.Platform.Profiles.Application.CommandServices;

/// <summary>
///     Restaurant profile command service interface
/// </summary>
public interface IRestaurantProfileCommandService
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
    ///     or the corresponding error otherwise
    /// </returns>
    Task<Result<RestaurantProfile>> Handle(CreateRestaurantProfileCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle the update of an existing restaurant profile
    /// </summary>
    Task<Result<RestaurantProfile>> Handle(UpdateRestaurantProfileCommand command, CancellationToken cancellationToken);
}
