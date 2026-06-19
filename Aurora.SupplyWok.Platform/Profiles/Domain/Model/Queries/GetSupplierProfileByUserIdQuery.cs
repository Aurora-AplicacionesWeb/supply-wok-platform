namespace Aurora.SupplyWok.Platform.Profiles.Domain.Model.Queries;
/// <summary>
///     Get restaurant profile by linked Iam user id query
/// </summary>
/// <param name="UserId">
///     The Iam user id to search for
/// </param>
public record GetSupplierProfileByUserIdQuery(int UserId);