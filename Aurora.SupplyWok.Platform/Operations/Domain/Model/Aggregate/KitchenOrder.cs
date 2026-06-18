using Aurora.SupplyWok.Platform.Operations.Domain.Model.ValueObjects;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.Entities;

namespace Aurora.SupplyWok.Platform.Operations.Domain.Model.Aggregate;

public partial class KitchenOrder
{
    public KitchenOrder(string number, int tableId, ETypeService typeService, string observations,
        DateOnly dateCreated)
    {
        Number = number;
        TableId = tableId;
        TypeService = typeService;
        Observations = observations;
        DateCreated = dateCreated;
    }

    public KitchenOrder(CreateKitchenOrderCommand command)
        : this(command.Number, command.TableId, command.TypeService, command.Observations, command.DateCreated)
    {
    }

    public int Id { get; }
    public string Number { get; private set; }
    public int TableId { get; private set; }
    public ETypeService TypeService { get; private set; }
    public EKitchenOrderStatus Status { get; private set; }
    public string Observations { get; private set; }
    public DateOnly DateCreated { get; private set; }
    public DateTime? HourReady { get; private set; }
    public DateTime? HourDelivered { get; private set; }
    public int PreparationTime { get; private set; }
    public double TotalPrice => Items.Sum(i => i.SubTotal);

    public void Update(string number, int tableId, ETypeService typeService, string observations)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Kitchen order number cannot be empty.", nameof(number));
        if (tableId <= 0)
            throw new ArgumentException("Table id must be greater than zero.", nameof(tableId));

        Number = number;
        TableId = tableId;
        TypeService = typeService;
        Observations = observations;
    }

    public KitchenOrderItem AddDish(int dishId, string dishName, int quantity, double unitPrice)
    {
        var existingItem = Items.FirstOrDefault(i => i.DishId == dishId);
        if (existingItem != null)
        {
            existingItem.UpdateQuantity(existingItem.Quantity + quantity);
            return existingItem;
        }

        var item = new KitchenOrderItem(dishId, dishName, quantity, unitPrice);
        Items.Add(item);
        return item;
    }

    public void RemoveDish(int kitchenOrderItemId)
    {
        var item = Items.FirstOrDefault(i => i.Id == kitchenOrderItemId);
        if (item is null)
            throw new ArgumentException($"Kitchen order item with ID {kitchenOrderItemId} not found.", nameof(kitchenOrderItemId));
        Items.Remove(item);
    }

    public void UpdateItemQuantity(int kitchenOrderItemId, int quantity)
    {
        var item = Items.FirstOrDefault(i => i.Id == kitchenOrderItemId);
        if (item is null)
            throw new ArgumentException($"Kitchen order item with ID {kitchenOrderItemId} not found.", nameof(kitchenOrderItemId));
        item.UpdateQuantity(quantity);
    }

    public void UpdateStatus(EKitchenOrderStatus newStatus)
    {
        if (Status == EKitchenOrderStatus.Delivered)
            throw new InvalidOperationException("Cannot change status of a delivered kitchen order.");

        if (Status == EKitchenOrderStatus.Cancelled)
            throw new InvalidOperationException("Cannot change status of a cancelled kitchen order.");

        if (newStatus == EKitchenOrderStatus.InPreparation && Status != EKitchenOrderStatus.Pending)
            throw new InvalidOperationException("Only pending orders can be moved to in preparation.");

        if (newStatus == EKitchenOrderStatus.Ready && Status != EKitchenOrderStatus.InPreparation)
            throw new InvalidOperationException("Only orders in preparation can be marked as ready.");

        if (newStatus == EKitchenOrderStatus.Delivered && Status != EKitchenOrderStatus.Ready)
            throw new InvalidOperationException("Only ready orders can be marked as delivered.");

        Status = newStatus;

        if (newStatus == EKitchenOrderStatus.Ready)
            HourReady = DateTime.Now;

        if (newStatus == EKitchenOrderStatus.Delivered)
        {
            HourDelivered = DateTime.Now;
            if (HourReady.HasValue)
                PreparationTime = (int)(HourDelivered.Value - HourReady.Value).TotalMinutes;
        }
    }
}
