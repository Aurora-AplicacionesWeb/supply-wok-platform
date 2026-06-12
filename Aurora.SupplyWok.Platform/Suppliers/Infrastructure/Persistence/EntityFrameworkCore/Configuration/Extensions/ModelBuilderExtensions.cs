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

        builder.Entity<Supplier>().ToTable("Suppliers");
        builder.Entity<Supplier>().HasKey(supplier => supplier.Id);
        builder.Entity<Supplier>().Property(supplier => supplier.Id).ValueGeneratedNever();
        builder.Entity<Supplier>().Property(supplier => supplier.Uuid).IsRequired();
        builder.Entity<Supplier>().HasIndex(supplier => supplier.Uuid).IsUnique();
        builder.Entity<Supplier>().Property(supplier => supplier.Name).IsRequired().HasMaxLength(100);
        builder.Entity<Supplier>().Property(supplier => supplier.ContactName).IsRequired().HasMaxLength(100);
        builder.Entity<Supplier>().Property(supplier => supplier.Email).IsRequired().HasMaxLength(100);
        builder.Entity<Supplier>().Property(supplier => supplier.Phone).IsRequired().HasMaxLength(30);
        builder.Entity<Supplier>().Property(supplier => supplier.Category).IsRequired().HasMaxLength(80);
        builder.Entity<Supplier>().Property(supplier => supplier.LinkedDate).IsRequired().HasMaxLength(10);
        builder.Entity<Supplier>().Property(supplier => supplier.Sla).IsRequired().HasMaxLength(20);
        builder.Entity<Supplier>().Property(supplier => supplier.ResponseTime).IsRequired().HasMaxLength(20);

        SeedSuppliers(builder);
    }

    private static void SeedSuppliers(ModelBuilder builder)
    {
        builder.Entity<Supplier>().HasData(
            new
            {
                Id = 201,
                Uuid = Guid.Parse("11111111-1111-1111-1111-111111111201"),
                Name = "Golden Wok Produce",
                ContactName = "Mariela Soto",
                Email = "msoto@goldenwok.pe",
                Phone = "+51 999 111 222",
                Category = "Grains and pantry",
                LinkedDate = "2026-04-21",
                Sla = "98% SLA",
                ResponseTime = "1.6 H"
            },
            new
            {
                Id = 202,
                Uuid = Guid.Parse("11111111-1111-1111-1111-111111111202"),
                Name = "Andes Cold Chain",
                ContactName = "Luis Cardenas",
                Email = "lcardenas@andescold.pe",
                Phone = "+51 999 333 444",
                Category = "Cold products",
                LinkedDate = "2026-04-20",
                Sla = "95% SLA",
                ResponseTime = "2.1 H"
            },
            new
            {
                Id = 203,
                Uuid = Guid.Parse("11111111-1111-1111-1111-111111111203"),
                Name = "Orient Pantry Co.",
                ContactName = "Zhen Liu",
                Email = "zliu@orientpantry.pe",
                Phone = "+51 999 555 666",
                Category = "Asian sauces and oils",
                LinkedDate = "2026-04-19",
                Sla = "91% SLA",
                ResponseTime = "2.9 H"
            });
    }
}
