using Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Spm.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

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
        builder.Entity<RestaurantReference>().ToTable("Clients");
        builder.Entity<RestaurantReference>().HasKey(client => client.Id);
        builder.Entity<RestaurantReference>().Property(client => client.Id).ValueGeneratedOnAdd();
        builder.Entity<RestaurantReference>().Property(client => client.Name).IsRequired().HasMaxLength(100);
        builder.Entity<RestaurantReference>().Property(client => client.District).IsRequired().HasMaxLength(80);
        builder.Entity<RestaurantReference>().Property(client => client.Status).IsRequired().HasMaxLength(20);

        builder.Entity<SupplierReference>().ToTable("Suppliers");
        builder.Entity<SupplierReference>().HasKey(supplier => supplier.Id);
        builder.Entity<SupplierReference>().Property(supplier => supplier.Id).ValueGeneratedNever();
        builder.Entity<SupplierReference>().Property(supplier => supplier.Uuid).IsRequired();
        builder.Entity<SupplierReference>().HasIndex(supplier => supplier.Uuid).IsUnique();
        builder.Entity<SupplierReference>().Property(supplier => supplier.Name).IsRequired().HasMaxLength(100);
        builder.Entity<SupplierReference>().Property(supplier => supplier.ContactName).IsRequired().HasMaxLength(100);
        builder.Entity<SupplierReference>().Property(supplier => supplier.Email).IsRequired().HasMaxLength(100);
        builder.Entity<SupplierReference>().Property(supplier => supplier.Phone).IsRequired().HasMaxLength(30);
        builder.Entity<SupplierReference>().Property(supplier => supplier.Category).IsRequired().HasMaxLength(80);
        builder.Entity<SupplierReference>().Property(supplier => supplier.LinkedDate).IsRequired().HasMaxLength(10);
        builder.Entity<SupplierReference>().Property(supplier => supplier.Sla).IsRequired().HasMaxLength(20);
        builder.Entity<SupplierReference>().Property(supplier => supplier.ResponseTime).IsRequired().HasMaxLength(20);

        builder.Entity<SupplierRestaurant>().ToTable("SupplierClients");
        builder.Entity<SupplierRestaurant>().HasKey(supplierClient => supplierClient.Id);
        builder.Entity<SupplierRestaurant>().Property(supplierClient => supplierClient.Id).ValueGeneratedOnAdd();
        builder.Entity<SupplierRestaurant>().Property(supplierClient => supplierClient.SupplierId).IsRequired();
        builder.Entity<SupplierRestaurant>().Property(supplierClient => supplierClient.ClientId).IsRequired();
        builder.Entity<SupplierRestaurant>()
            .HasIndex(supplierClient => new { supplierClient.SupplierId, supplierClient.ClientId })
            .IsUnique();
        builder.Entity<SupplierRestaurant>()
            .HasOne<SupplierReference>()
            .WithMany()
            .HasForeignKey(supplierClient => supplierClient.SupplierId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<SupplierRestaurant>()
            .HasOne<RestaurantReference>()
            .WithMany()
            .HasForeignKey(supplierClient => supplierClient.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<CatalogItem>().ToTable("CatalogItems");
        builder.Entity<CatalogItem>().HasKey(catalogItem => catalogItem.Id);
        builder.Entity<CatalogItem>().Property(catalogItem => catalogItem.Id).ValueGeneratedOnAdd();
        builder.Entity<CatalogItem>().Property(catalogItem => catalogItem.SupplierId).IsRequired();
        builder.Entity<CatalogItem>().Property(catalogItem => catalogItem.Name).IsRequired().HasMaxLength(100);
        builder.Entity<CatalogItem>().Property(catalogItem => catalogItem.Category).IsRequired().HasMaxLength(80);
        builder.Entity<CatalogItem>().Property(catalogItem => catalogItem.Price).IsRequired().HasPrecision(18, 2);
        builder.Entity<CatalogItem>().Property(catalogItem => catalogItem.Unit).IsRequired().HasConversion<string>().HasColumnType("longtext").IsUnicode(false);
        builder.Entity<CatalogItem>().Property(catalogItem => catalogItem.DeliveryConditions).IsRequired().HasMaxLength(250);
        builder.Entity<CatalogItem>().HasIndex(catalogItem => catalogItem.SupplierId);
        builder.Entity<CatalogItem>()
            .HasOne<SupplierReference>()
            .WithMany()
            .HasForeignKey(catalogItem => catalogItem.SupplierId)
            .OnDelete(DeleteBehavior.Cascade);

        SeedSuppliers(builder);
    }

    private static void SeedSuppliers(ModelBuilder builder)
    {
        builder.Entity<SupplierReference>().HasData(
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
                ResponseTime = "1.6 H",
                CreatedAt = (DateTimeOffset?)null,
                UpdatedAt = (DateTimeOffset?)null
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
                ResponseTime = "2.1 H",
                CreatedAt = (DateTimeOffset?)null,
                UpdatedAt = (DateTimeOffset?)null
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
                ResponseTime = "2.9 H",
                CreatedAt = (DateTimeOffset?)null,
                UpdatedAt = (DateTimeOffset?)null
            });
    }
}
