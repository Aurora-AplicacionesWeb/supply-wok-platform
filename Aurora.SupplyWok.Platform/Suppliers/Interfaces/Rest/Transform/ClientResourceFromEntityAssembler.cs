using Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Suppliers.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Suppliers.Interfaces.Rest.Transform;

/// <summary>
///     Maps client aggregates to REST resources.
/// </summary>
public static class ClientResourceFromEntityAssembler
{
    /// <summary>
    ///     Maps a client aggregate to its REST representation.
    /// </summary>
    /// <param name="client">The client aggregate.</param>
    /// <returns>The client resource.</returns>
    public static ClientResource ToResourceFromEntity(Client client)
    {
        ArgumentNullException.ThrowIfNull(client);

        return new ClientResource(
            client.Id,
            client.Name,
            client.District,
            client.Status);
    }
}
