namespace Aurora.SupplyWok.Platform.Profiles.Domain.Model.Queries;

/// <summary>
///     Get supplier profile by id query
/// </summary>
/// <param name="SupplierProfileId">
///     The id of the supplier profile to retrieve
/// </param>
public record GetSupplierProfileByIdQuery(int SupplierProfileId);