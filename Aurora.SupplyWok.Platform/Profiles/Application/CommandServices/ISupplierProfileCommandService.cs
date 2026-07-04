using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Shared.Application.Model;

namespace Aurora.SupplyWok.Platform.Profiles.Application.CommandServices;

/// <summary>
///     Supplier profile command service interface
/// </summary>
public interface ISupplierProfileCommandService
{
    /// <summary>
    ///     Handle the creation of a new supplier profile
    /// </summary>
    /// <param name="command">
    ///     The <see cref="CreateSupplierProfileCommand" /> with the data of the supplier profile to create
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A <see cref="Result{T}" /> with the created <see cref="SupplierProfile" /> if successful,
    ///     or the corresponding error otherwise
    /// </returns>
    Task<Result<SupplierProfile>> Handle(CreateSupplierProfileCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle the update of an existing supplier profile
    /// </summary>
    Task<Result<SupplierProfile>> Handle(UpdateSupplierProfileCommand command, CancellationToken cancellationToken);
}
