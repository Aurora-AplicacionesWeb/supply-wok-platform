using Aurora.SupplyWok.Platform.Suppliers.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Suppliers.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
///     Entity Framework model configuration extensions for the Suppliers bounded context.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the supplier client persistence configuration.
    /// </summary>
    /// <param name="builder">The EF model builder.</param>
    public static void ApplySupplierConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Client>().ToTable("Clients");
        builder.Entity<Client>().HasKey(client => client.Id);
        builder.Entity<Client>().Property(client => client.Id).ValueGeneratedOnAdd();
        builder.Entity<Client>().Property(client => client.Name).IsRequired().HasMaxLength(100);
        builder.Entity<Client>().Property(client => client.District).IsRequired().HasMaxLength(80);
        builder.Entity<Client>().Property(client => client.Status).IsRequired().HasMaxLength(20);
    }
}
