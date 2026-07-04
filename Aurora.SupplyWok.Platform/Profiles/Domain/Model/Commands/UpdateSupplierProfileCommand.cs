using Aurora.SupplyWok.Platform.Profiles.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Profiles.Domain.Model.Commands;

/// <summary>
///     Update Supplier Profile Command
/// </summary>
/// <param name="Id">The supplier profile identifier.</param>
/// <param name="BusinessName">The business name of the supplier.</param>
/// <param name="ContactName">The contact person name.</param>
/// <param name="Address">The supplier address.</param>
/// <param name="ContactEmail">The contact email.</param>
/// <param name="Phone">The supplier contact phone.</param>
/// <param name="Category">The supplier category.</param>
public record UpdateSupplierProfileCommand(
    int Id,
    string BusinessName,
    PersonName ContactName,
    StreetAddress Address,
    EmailAddress ContactEmail,
    string Phone,
    string Category);
