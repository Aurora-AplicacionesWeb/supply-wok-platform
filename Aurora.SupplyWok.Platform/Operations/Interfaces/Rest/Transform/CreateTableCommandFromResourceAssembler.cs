using Aurora.SupplyWok.Platform.Operations.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Operations.Interfaces.Rest.Resources;
namespace Aurora.SupplyWok.Platform.Operations.Interfaces.Rest.Transform;

/// <summary>
/// Assembler responsible for transforming a <see cref="CreateTableResource"/> to a <see cref="CreateTableCommand"/> 
/// </summary>
public static class CreateTableCommandFromResourceAssembler
{
    /// <summary>
    /// Converts a <see cref="CreateTableResource"/> to a <see cref="CreateTableCommand"/>
    /// </summary>
    /// <param name="resource">
    /// The <see cref="CreateTableResource"/> containing the data for creating a table. Must not be null
    /// </param>
    /// <returns>
    /// A new <see cref="CreateTableCommand"/> instance 
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if the input <paramref name="resource"/> is nill.</exception>
    public static CreateTableCommand ToCommandFromResource(CreateTableResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource),
                "CreateTableResource cannot be null when converting to command.");
        return new CreateTableCommand(
            resource.Number, 
            resource.Capacity, 
            resource.Location, 
            resource.State, 
            resource.Active);
    }
}