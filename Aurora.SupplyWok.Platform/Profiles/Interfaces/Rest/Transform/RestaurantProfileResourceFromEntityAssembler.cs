using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Profiles.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Profiles.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to transform a <see cref="RestaurantProfile" /> into a <see cref="RestaurantProfileResource" />
/// </summary>
public static class RestaurantProfileResourceFromEntityAssembler
{
    /// <summary>
    ///     Build a <see cref="RestaurantProfileResource" /> from a <see cref="RestaurantProfile" />
    /// </summary>
    /// <param name="restaurantProfile">
    ///     The restaurant profile entity to transform
    /// </param>
    /// <returns>
    ///     The corresponding <see cref="RestaurantProfileResource" />
    /// </returns>
    public static RestaurantProfileResource ToResourceFromEntity(RestaurantProfile restaurantProfile)
    {
        ArgumentNullException.ThrowIfNull(restaurantProfile);

        return new RestaurantProfileResource(
            restaurantProfile.Id,
            restaurantProfile.BusinessName,
            restaurantProfile.ContactName.FirstName,
            restaurantProfile.ContactName.LastName,
            restaurantProfile.Address.Street,
            restaurantProfile.Address.District,
            restaurantProfile.Address.City,
            restaurantProfile.Address.Country,
            restaurantProfile.ContactEmail.Address,
            restaurantProfile.Status,
            restaurantProfile.UserId);
    }
}
