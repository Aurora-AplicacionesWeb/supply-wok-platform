using Aurora.SupplyWok.Platform.Operations.Domain.Model.ValueObjects;
namespace Aurora.SupplyWok.Platform.Operations.Interfaces.Acl;

/// <summary>
/// Facade for the Operations context
/// </summary>
public interface IOperationsContextFacade
{
    Task<int> CreateTable(string number, 
        int capacity, 
        string location, 
        ETableStatus state, 
        bool active, 
        CancellationToken cancellationToken);
    
}