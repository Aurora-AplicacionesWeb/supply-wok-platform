using Aurora.SupplyWok.Platform.Operations.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;
namespace Aurora.SupplyWok.Platform.Operations.Domain.Repositories;

/// <summary>
/// Represents the Table repository in the Supply Wok Platform.
/// </summary>
public interface ITableRepository : IBaseRepository<Table>
{
    /// <summary>
    /// Find a table by id
    /// </summary>
    /// <param name="id">
    /// The id of the table to search for
    /// </param>
    /// <param name="cancellationToken">
    /// The cancellation token
    /// </param>
    /// <returns>
    /// The <see cref="Table"/> if found, otherwise null
    /// </returns>
    Task<Table?> GetTableByIdAsync(int id, CancellationToken cancellationToken);
}