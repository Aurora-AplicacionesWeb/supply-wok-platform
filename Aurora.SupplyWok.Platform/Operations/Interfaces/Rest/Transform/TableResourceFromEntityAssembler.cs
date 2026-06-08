using Aurora.SupplyWok.Platform.Iot.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Operations.Interfaces.Rest.Resources;
namespace Aurora.SupplyWok.Platform.Operations.Interfaces.Rest.Transform;

/// <summary>
/// Assembler responsible for transforming a <see cref="Table"/> entity to a <see cref="TableResource"/> for REST representation.
/// </summary>
public static class TableResourceFromEntityAssembler
{
    /// <summary>
    /// Transform a <see cref="Table"/> entity to a <see cref="TableResource"/> representation.
    /// </summary>
    /// <param name="entity">
    /// The <see cref="Table"/> entity to convert. Must not be null.
    /// </param>
    /// <returns>
    /// A <see cref="TableResource"/> instance containing the data from the provided <see cref="Table"/> entity.
    /// </returns>
    /// <exception cref="ArgumentNullException"> Thrown if the input <paramref name="entity"/>is null.</exception>
    public static TableResource ToResourceFromEntity(Table table) {
        if (table == null)
            throw new ArgumentNullException(nameof(table),
                "Table entity cannot be null when converting to resource.");
        return new TableResource(table.Id, 
            table.Number, 
            table.Capacity, 
            table.Location, 
            table.State, 
            table.Active);
    }
}
