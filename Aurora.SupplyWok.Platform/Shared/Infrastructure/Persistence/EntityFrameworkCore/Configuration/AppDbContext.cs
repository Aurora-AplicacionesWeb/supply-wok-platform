using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using Microsoft.EntityFrameworkCore;
using Aurora.SupplyWok.Platform.Iot.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Aurora.SupplyWok.Platform.Purchasing.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Aurora.SupplyWok.Platform.Operations.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

namespace Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

/// <summary>
///     Application database context for the Learning Center Platform
/// </summary>
/// <param name="options">
///     The options for the database context
/// </param>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Apply audit timestamp interceptor for all IAuditableEntity implementations
        builder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(builder);
    }
    
    /// <summary>
    ///     On creating the database model
    /// </summary>
    /// <remarks>
    ///     This method is used to create the database model for the application.
    /// </remarks>
    /// <param name="builder">
    ///     The model builder for the database context
    /// </param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Iot Context
        builder.ApplySensorsConfiguration();
        builder.ApplyAlertsConfiguration();

        // Purchasing Context
        builder.ApplyPurchasingConfiguration();

        // Operations Context
        builder.ApplyOperationsConfiguration();

        // General Naming Convention for the database objects
        builder.UseSnakeCaseNamingConvention();
    }
}
