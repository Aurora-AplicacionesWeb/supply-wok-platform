using Aurora.SupplyWok.Platform.Purchasing.Application.CommandServices;
using Aurora.SupplyWok.Platform.Purchasing.Domain.Model;
using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.ValueObjects;
using Aurora.SupplyWok.Platform.Purchasing.Domain.Repositories;
using Aurora.SupplyWok.Platform.Shared.Application.Model;
using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;
using Aurora.SupplyWok.Platform.Suppliers.Interfaces.Acl;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Purchasing.Application.Internal.CommandServices;

public class PurchaseOrderCommandService(
    IPurchaseOrderRepository purchaseOrderRepository,
    ISupplierContextFacade supplierContextFacade,
    IUnitOfWork unitOfWork) : IPurchaseOrderCommandService
{
    public async Task<Result<PurchaseOrder>> Handle(CreatePurchaseOrderCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var supplierIdentity = await supplierContextFacade.GetSupplierIdentityById(command.SupplierId, cancellationToken);
            var validation = await ValidateOrderData(command.Code, command.RestaurantName, null,
                command.SupplierId, command.OrderDate, command.EstimatedDate, command.Priority, command.Status ?? "Pending",
                command.Items, supplierIdentity, cancellationToken);
            if (validation is not null) return Result<PurchaseOrder>.Failure(validation.Value.Error, validation.Value.Message);

            var priority = ParsePriority(command.Priority);
            var status = ParseStatus(command.Status ?? "Pending");
            if (status != EPurchaseOrderStatus.Pending)
                return Result<PurchaseOrder>.Failure(PurchaseOrdersError.InvalidData, "New purchase orders must start as Pending.");

            var order = new PurchaseOrder(command.Code.Trim(), command.SupplierId, supplierIdentity!.Name.Trim(),
                command.RestaurantName.Trim(), command.OrderDate.Trim(), command.EstimatedDate?.Trim(), priority, status,
                ToItems(command.Items));

            await purchaseOrderRepository.AddAsync(order, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<PurchaseOrder>.Success(order);
        }
        catch (OperationCanceledException)
        {
            return Result<PurchaseOrder>.Failure(PurchaseOrdersError.OperationCancelled, nameof(PurchaseOrdersError.OperationCancelled));
        }
        catch (DbUpdateException)
        {
            return Result<PurchaseOrder>.Failure(PurchaseOrdersError.DatabaseError, nameof(PurchaseOrdersError.DatabaseError));
        }
        catch (Exception ex)
        {
            return Result<PurchaseOrder>.Failure(PurchaseOrdersError.InternalServerError, ex.Message);
        }
    }

    public async Task<Result<PurchaseOrder>> Handle(UpdatePurchaseOrderCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var order = await purchaseOrderRepository.GetPurchaseOrderByIdAsync(command.Id, cancellationToken);
            if (order is null)
                return Result<PurchaseOrder>.Failure(PurchaseOrdersError.PurchaseOrderNotFound, nameof(PurchaseOrdersError.PurchaseOrderNotFound));

            var supplierIdentity = await supplierContextFacade.GetSupplierIdentityById(command.SupplierId, cancellationToken);
            var validation = await ValidateOrderData(command.Code, command.RestaurantName,
                command.Id, command.SupplierId, command.OrderDate, command.EstimatedDate, command.Priority, command.Status,
                command.Items, supplierIdentity, cancellationToken);
            if (validation is not null) return Result<PurchaseOrder>.Failure(validation.Value.Error, validation.Value.Message);

            var nextStatus = ParseStatus(command.Status);
            if (!order.CanTransitionTo(nextStatus))
                return Result<PurchaseOrder>.Failure(PurchaseOrdersError.InvalidStatusTransition, nameof(PurchaseOrdersError.InvalidStatusTransition));

            order.Update(command.Code.Trim(), command.SupplierId, supplierIdentity!.Name.Trim(), command.RestaurantName.Trim(),
                command.OrderDate.Trim(), command.EstimatedDate?.Trim(), ParsePriority(command.Priority), nextStatus,
                ToItems(command.Items));

            purchaseOrderRepository.Update(order);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<PurchaseOrder>.Success(order);
        }
        catch (OperationCanceledException)
        {
            return Result<PurchaseOrder>.Failure(PurchaseOrdersError.OperationCancelled, nameof(PurchaseOrdersError.OperationCancelled));
        }
        catch (DbUpdateException)
        {
            return Result<PurchaseOrder>.Failure(PurchaseOrdersError.DatabaseError, nameof(PurchaseOrdersError.DatabaseError));
        }
        catch (Exception ex)
        {
            return Result<PurchaseOrder>.Failure(PurchaseOrdersError.InternalServerError, ex.Message);
        }
    }

    public async Task<Result<bool>> Handle(DeletePurchaseOrderCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var order = await purchaseOrderRepository.GetPurchaseOrderByIdAsync(command.Id, cancellationToken);
            if (order is null)
                return Result<bool>.Failure(PurchaseOrdersError.PurchaseOrderNotFound, nameof(PurchaseOrdersError.PurchaseOrderNotFound));

            purchaseOrderRepository.Remove(order);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<bool>.Success(true);
        }
        catch (OperationCanceledException)
        {
            return Result<bool>.Failure(PurchaseOrdersError.OperationCancelled, nameof(PurchaseOrdersError.OperationCancelled));
        }
        catch (DbUpdateException)
        {
            return Result<bool>.Failure(PurchaseOrdersError.DatabaseError, nameof(PurchaseOrdersError.DatabaseError));
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(PurchaseOrdersError.InternalServerError, ex.Message);
        }
    }

    public async Task<Result<PurchaseOrder>> Handle(UpdatePurchaseOrderStatusCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var order = await purchaseOrderRepository.GetPurchaseOrderByIdAsync(command.Id, cancellationToken);
            if (order is null)
                return Result<PurchaseOrder>.Failure(PurchaseOrdersError.PurchaseOrderNotFound, nameof(PurchaseOrdersError.PurchaseOrderNotFound));

            if (!TryParseStatus(command.Status, out var status))
                return Result<PurchaseOrder>.Failure(PurchaseOrdersError.InvalidData, "Invalid purchase order status.");

            if (!order.CanTransitionTo(status))
                return Result<PurchaseOrder>.Failure(PurchaseOrdersError.InvalidStatusTransition, nameof(PurchaseOrdersError.InvalidStatusTransition));

            order.UpdateStatus(status);
            purchaseOrderRepository.Update(order);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<PurchaseOrder>.Success(order);
        }
        catch (OperationCanceledException)
        {
            return Result<PurchaseOrder>.Failure(PurchaseOrdersError.OperationCancelled, nameof(PurchaseOrdersError.OperationCancelled));
        }
        catch (DbUpdateException)
        {
            return Result<PurchaseOrder>.Failure(PurchaseOrdersError.DatabaseError, nameof(PurchaseOrdersError.DatabaseError));
        }
        catch (Exception ex)
        {
            return Result<PurchaseOrder>.Failure(PurchaseOrdersError.InternalServerError, ex.Message);
        }
    }

    private async Task<(PurchaseOrdersError Error, string Message)?> ValidateOrderData(
        string code,
        string restaurantName,
        int? excludedId,
        int supplierId,
        string orderDate,
        string? estimatedDate,
        string priority,
        string status,
        IEnumerable<CreatePurchaseOrderItemCommand> items,
        SupplierIdentityAclResource? supplierIdentity,
        CancellationToken cancellationToken)
    {
        var itemList = items.ToList();

        if (string.IsNullOrWhiteSpace(code)) return (PurchaseOrdersError.InvalidData, "Purchase order code is required.");
        if (string.IsNullOrWhiteSpace(restaurantName)) return (PurchaseOrdersError.InvalidData, "Restaurant name is required.");
        if (supplierId <= 0) return (PurchaseOrdersError.InvalidData, "Supplier is required.");
        if (string.IsNullOrWhiteSpace(orderDate) || !DateOnly.TryParse(orderDate, out var parsedOrderDate))
            return (PurchaseOrdersError.InvalidData, "Order date is required and must be valid.");
        if (!string.IsNullOrWhiteSpace(estimatedDate) && DateOnly.TryParse(estimatedDate, out var parsedEstimatedDate) && parsedEstimatedDate < parsedOrderDate)
            return (PurchaseOrdersError.InvalidData, "Estimated date cannot be earlier than order date.");
        if (!string.IsNullOrWhiteSpace(estimatedDate) && !DateOnly.TryParse(estimatedDate, out _))
            return (PurchaseOrdersError.InvalidData, "Estimated date must be valid.");
        if (!TryParsePriority(priority, out _)) return (PurchaseOrdersError.InvalidData, "Invalid purchase order priority.");
        if (!TryParseStatus(status, out _)) return (PurchaseOrdersError.InvalidData, "Invalid purchase order status.");
        if (itemList.Count == 0) return (PurchaseOrdersError.InvalidData, "At least one purchase order item is required.");
        if (supplierIdentity is null)
            return (PurchaseOrdersError.SupplierNotFound, nameof(PurchaseOrdersError.SupplierNotFound));
        if (await purchaseOrderRepository.ExistsByCodeAsync(code.Trim(), excludedId, cancellationToken))
            return (PurchaseOrdersError.DuplicateCode, nameof(PurchaseOrdersError.DuplicateCode));

        foreach (var item in itemList)
        {
            if (string.IsNullOrWhiteSpace(item.ProductName)) return (PurchaseOrdersError.InvalidData, "Product name is required.");
            if (item.Quantity <= 0) return (PurchaseOrdersError.InvalidData, "Quantity must be greater than zero.");
            if (item.UnitPrice <= 0) return (PurchaseOrdersError.InvalidData, "Unit price must be greater than zero.");
            if (string.IsNullOrWhiteSpace(item.UnitType)) return (PurchaseOrdersError.InvalidData, "Unit type is required.");
        }

        return null;
    }

    private static IEnumerable<PurchaseOrderItem> ToItems(IEnumerable<CreatePurchaseOrderItemCommand> items)
    {
        return items.Select(item => new PurchaseOrderItem(item.InventoryItemId, item.ProductName.Trim(), item.Quantity,
            item.UnitPrice, item.UnitType.Trim()));
    }

    private static EPurchaseOrderPriority ParsePriority(string value)
    {
        TryParsePriority(value, out var priority);
        return priority;
    }

    private static EPurchaseOrderStatus ParseStatus(string value)
    {
        TryParseStatus(value, out var status);
        return status;
    }

    private static bool TryParsePriority(string value, out EPurchaseOrderPriority priority)
    {
        return Enum.TryParse(value?.Trim(), true, out priority);
    }

    private static bool TryParseStatus(string value, out EPurchaseOrderStatus status)
    {
        var normalized = value?.Replace(" ", string.Empty).Replace("-", string.Empty).Trim();
        return Enum.TryParse(normalized, true, out status);
    }
}
