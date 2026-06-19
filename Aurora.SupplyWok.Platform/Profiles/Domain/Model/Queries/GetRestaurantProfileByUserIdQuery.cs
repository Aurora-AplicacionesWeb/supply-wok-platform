namespace Aurora.SupplyWok.Platform.Profiles.Domain.Model.Queries;

/// <summary>
///     Get restaurant profile by linked Iam user id query
/// </summary>
/// <param name="UserId">
///     The Iam user id linked to the restaurant profile to retrieve
/// </param>
public record GetRestaurantProfileByUserIdQuery(int UserId);