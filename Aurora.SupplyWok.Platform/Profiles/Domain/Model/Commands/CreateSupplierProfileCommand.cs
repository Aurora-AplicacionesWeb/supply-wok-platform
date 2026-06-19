using Aurora.SupplyWok.Platform.Profiles.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Profiles.Domain.Model.Commands;

/// <summary>
///     Create Supplier Profile Command
/// </summary>
/// <param name="BusinessName">
///     The legal/commercial name of the supplier.
/// </param>
/// <param name="ContactName">
///     The <see cref="ValueObjects.PersonName" /> of the supplier's contact person.
/// </param>
/// <param name="Address">
///     The <see cref="ValueObjects.StreetAddress" /> of the supplier.
/// </param>
/// <param name="ContactEmail">
///     The <see cref="ValueObjects.EmailAddress" /> of the supplier's contact person.
/// </param>
/// <param name="UserId">
///     Optional placeholder for a future Iam user identifier. Null while Iam is not integrated.
/// </param>
public record CreateSupplierProfileCommand(string BusinessName,
    PersonName ContactName,
    StreetAddress Address,
    EmailAddress ContactEmail,
    int? UserId = null);