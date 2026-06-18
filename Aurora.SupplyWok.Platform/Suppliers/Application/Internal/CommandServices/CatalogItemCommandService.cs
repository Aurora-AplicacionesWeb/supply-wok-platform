using Aurora.SupplyWok.Platform.Shared.Application.Model;
using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;
using Aurora.SupplyWok.Platform.Suppliers.Application.CommandServices;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Model;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Suppliers.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Suppliers.Application.Internal.CommandServices;

public class CatalogItemCommandService(
    ICatalogItemRepository catalogItemRepository,
    ISupplierRepository supplierRepository,
    IUnitOfWork unitOfWork) : ICatalogItemCommandService
{
    public async Task<Result<CatalogItem>> Handle(CreateCatalogItemCommand command, CancellationToken cancellationToken)
    {
        try
        {
            if (!await supplierRepository.ExistsByIdAsync(command.SupplierId, cancellationToken))
                return Result<CatalogItem>.Failure(SuppliersError.SupplierNotFound, nameof(SuppliersError.SupplierNotFound));

            var catalogItem = new CatalogItem(command);
            await catalogItemRepository.AddAsync(catalogItem, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<CatalogItem>.Success(catalogItem);
        }
        catch (ArgumentException ex)
        {
            return Result<CatalogItem>.Failure(SuppliersError.InvalidData, ex.Message);
        }
        catch (OperationCanceledException)
        {
            return Result<CatalogItem>.Failure(SuppliersError.OperationCancelled, nameof(SuppliersError.OperationCancelled));
        }
        catch (DbUpdateException)
        {
            return Result<CatalogItem>.Failure(SuppliersError.DatabaseError, nameof(SuppliersError.DatabaseError));
        }
        catch (Exception ex)
        {
            return Result<CatalogItem>.Failure(SuppliersError.InternalServerError, ex.Message);
        }
    }

    public async Task<Result<CatalogItem>> Handle(UpdateCatalogItemCommand command, CancellationToken cancellationToken)
    {
        try
        {
            if (!await supplierRepository.ExistsByIdAsync(command.SupplierId, cancellationToken))
                return Result<CatalogItem>.Failure(SuppliersError.SupplierNotFound, nameof(SuppliersError.SupplierNotFound));

            var catalogItem = await catalogItemRepository.FindByIdAndSupplierIdAsync(command.CatalogItemId, command.SupplierId, cancellationToken);
            if (catalogItem is null)
                return Result<CatalogItem>.Failure(SuppliersError.CatalogItemNotFound, nameof(SuppliersError.CatalogItemNotFound));

            catalogItem.Update(command.Name, command.Category, command.Price, command.Unit, command.DeliveryConditions);
            catalogItemRepository.Update(catalogItem);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<CatalogItem>.Success(catalogItem);
        }
        catch (ArgumentException ex)
        {
            return Result<CatalogItem>.Failure(SuppliersError.InvalidData, ex.Message);
        }
        catch (OperationCanceledException)
        {
            return Result<CatalogItem>.Failure(SuppliersError.OperationCancelled, nameof(SuppliersError.OperationCancelled));
        }
        catch (DbUpdateException)
        {
            return Result<CatalogItem>.Failure(SuppliersError.DatabaseError, nameof(SuppliersError.DatabaseError));
        }
        catch (Exception ex)
        {
            return Result<CatalogItem>.Failure(SuppliersError.InternalServerError, ex.Message);
        }
    }

    public async Task<Result<bool>> Handle(DeleteCatalogItemCommand command, CancellationToken cancellationToken)
    {
        try
        {
            if (!await supplierRepository.ExistsByIdAsync(command.SupplierId, cancellationToken))
                return Result<bool>.Failure(SuppliersError.SupplierNotFound, nameof(SuppliersError.SupplierNotFound));

            var catalogItem = await catalogItemRepository.FindByIdAndSupplierIdAsync(command.CatalogItemId, command.SupplierId, cancellationToken);
            if (catalogItem is null)
                return Result<bool>.Failure(SuppliersError.CatalogItemNotFound, nameof(SuppliersError.CatalogItemNotFound));

            catalogItemRepository.Remove(catalogItem);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<bool>.Success(true);
        }
        catch (OperationCanceledException)
        {
            return Result<bool>.Failure(SuppliersError.OperationCancelled, nameof(SuppliersError.OperationCancelled));
        }
        catch (DbUpdateException)
        {
            return Result<bool>.Failure(SuppliersError.DatabaseError, nameof(SuppliersError.DatabaseError));
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(SuppliersError.InternalServerError, ex.Message);
        }
    }
}
