using Aurora.SupplyWok.Platform.Profiles.Application.CommandServices;
using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Profiles.Domain.Model;
using Aurora.SupplyWok.Platform.Profiles.Domain.Repositories;
using Aurora.SupplyWok.Platform.Shared.Application.Model;
using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Profiles.Application.Internal.CommandServices;

/// <summary>
///     Supplier profile command service implementation
/// </summary>
/// <remarks>
///     Implements <see cref="ISupplierProfileCommandService" /> to handle supplier profile commands
/// </remarks>
public class SupplierProfileCommandService(
    ISupplierProfileRepository supplierProfileRepository,
    IUnitOfWork unitOfWork) : ISupplierProfileCommandService
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
    ///     or the corresponding <see cref="ProfilesError" /> otherwise
    /// </returns>
    public async Task<Result<SupplierProfile>> Handle(CreateSupplierProfileCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var supplierProfile = new SupplierProfile(command);
            await supplierProfileRepository.AddAsync(supplierProfile, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<SupplierProfile>.Success(supplierProfile);
        }
        catch (ArgumentException ex)
        {
            return Result<SupplierProfile>.Failure(ProfilesError.InvalidData, ex.Message);
        }
        catch (OperationCanceledException)
        {
            return Result<SupplierProfile>.Failure(ProfilesError.OperationCancelled, nameof(ProfilesError.OperationCancelled));
        }
        catch (DbUpdateException)
        {
            return Result<SupplierProfile>.Failure(ProfilesError.DatabaseError, nameof(ProfilesError.DatabaseError));
        }
        catch (Exception ex)
        {
            return Result<SupplierProfile>.Failure(ProfilesError.InternalServerError, ex.Message);
        }
    }
}
