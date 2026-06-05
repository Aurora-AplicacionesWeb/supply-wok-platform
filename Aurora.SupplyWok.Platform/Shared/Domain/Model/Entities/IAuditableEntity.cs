namespace Aurora.SupplyWok.Platform.shared.domain.Model.Entities;
/// <summary>
///     Marks an entity as carrying audit timestamps managed by the persistence layer.
/// </summary>
/// <remarks>
///     Any entity in any bounded context that implements this interface will automatically
///     have <see cref="CreatedAt" /> set once on first persistence and <see cref="UpdatedAt" />
///     refreshed on every subsequent save, via <c>AuditableEntityInterceptor</c>.
/// </remarks>
public class IAuditableEntity
{
    /// <summary>
    ///     Gets or sets the UTC timestamp when the entity was first persisted.
    /// </summary>
    private DateTimeOffset? CreaetedAt { get; set; }
    /// <summary>
    ///     Gets or sets the UTC timestamp when the entity was last saved.
    /// </summary>
    private DateTimeOffset? UpdatedAt { get; set; }
}