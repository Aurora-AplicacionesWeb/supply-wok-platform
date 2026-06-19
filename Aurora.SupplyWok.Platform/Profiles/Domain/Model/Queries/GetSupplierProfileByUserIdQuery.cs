namespace Aurora.SupplyWok.Platform.Profiles.Domain.Model.Queries;

/// <summary>
///     Get supplier profile by linked Iam user id query
/// </summary>
/// <param name="UserId">
///     The Iam user id linked to the supplier profile to retrieve
/// </param>
public record GetSupplierProfileByUserIdQuery(int UserId);