namespace Aurora.SupplyWok.Platform.Profiles.Domain.Model.Queries;

/// <summary>
///     Get restaurant profile by id query
/// </summary>
/// <param name="RestaurantProfileId">
///     The id of the restaurant profile to retrieve
/// </param>
public record GetRestaurantProfileByIdQuery(int RestaurantProfileId);