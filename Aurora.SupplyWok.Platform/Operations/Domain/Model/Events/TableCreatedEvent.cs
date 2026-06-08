using Aurora.SupplyWok.Platform.Operations.Domain.Model.ValueObjects;
using Aurora.SupplyWok.Platform.Shared.Domain.Model.Events;
namespace Aurora.SupplyWok.Platform.Operations.Domain.Model.Events;

public class TableCreatedEvent(string number, int capacity, string location, ETableStatus state, bool active) : IEvent
{
    public string Number { get; } = number;
    public int Capacity { get; } = capacity;
    public string Location { get; } = location;
    public ETableStatus State { get; } = state;
    public bool Active { get; } = active;   
}