using Aurora.SupplyWok.Platform.Operations.Domain.Model.ValueObjects;
namespace Aurora.SupplyWok.Platform.Operations.Interfaces.Rest.Resources;

public record UpdateTableResource(string Number, 
    int Capacity, 
    string Location, 
    ETableStatus State, 
    bool Active);