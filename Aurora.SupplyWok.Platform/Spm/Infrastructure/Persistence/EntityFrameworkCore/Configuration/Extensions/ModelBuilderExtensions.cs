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
        builder.Entity<SupplierRestaurant>().ToTable("SupplierRestaurants");
        builder.Entity<SupplierRestaurant>().HasKey(supplierClient => supplierClient.Id);
        builder.Entity<SupplierRestaurant>().Property(supplierClient => supplierClient.Id).ValueGeneratedOnAdd();
        builder.Entity<SupplierRestaurant>().Property(supplierClient => supplierClient.SupplierProfileId).IsRequired();
        builder.Entity<SupplierRestaurant>().Property(supplierClient => supplierClient.RestaurantProfileId).IsRequired();
        builder.Entity<SupplierRestaurant>().Property(supplierClient => supplierClient.LinkedDate).IsRequired().HasMaxLength(10);
        builder.Entity<SupplierRestaurant>().Property(supplierClient => supplierClient.Status).IsRequired().HasMaxLength(20);
        builder.Entity<SupplierRestaurant>().Property(supplierClient => supplierClient.Sla).IsRequired().HasMaxLength(20);
        builder.Entity<SupplierRestaurant>().Property(supplierClient => supplierClient.ResponseTime).IsRequired().HasMaxLength(20);
        builder.Entity<SupplierRestaurant>()
            .HasIndex(supplierClient => new { supplierClient.SupplierProfileId, supplierClient.RestaurantProfileId }).HasDatabaseName("ix_supplier_restaurants_supplier_profile_restaurant_profile")
            .IsUnique();

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

        SeedSupplierRestaurants(builder);
    }

    private static void SeedSupplierRestaurants(ModelBuilder builder)
    {
        builder.Entity<SupplierRestaurant>().HasData(
            new
            {
                Id = 1,
                SupplierProfileId = 201,
                RestaurantProfileId = 1,
                LinkedDate = "2026-04-21",
                Status = "Active",
                Sla = "98% SLA",
                ResponseTime = "1.6 H",
                CreatedAt = (DateTimeOffset?)null,
                UpdatedAt = (DateTimeOffset?)null
            },
            new
            {
                Id = 2,
                SupplierProfileId = 202,
                RestaurantProfileId = 1,
                LinkedDate = "2026-04-20",
                Status = "Active",
                Sla = "95% SLA",
                ResponseTime = "2.1 H",
                CreatedAt = (DateTimeOffset?)null,
                UpdatedAt = (DateTimeOffset?)null
            },
            new
            {
                Id = 3,
                SupplierProfileId = 203,
                RestaurantProfileId = 2,
                LinkedDate = "2026-04-19",
                Status = "Active",
                Sla = "91% SLA",
                ResponseTime = "2.9 H",
                CreatedAt = (DateTimeOffset?)null,
                UpdatedAt = (DateTimeOffset?)null
            },
            new
            {
                Id = 4,
                SupplierProfileId = 201,
                RestaurantProfileId = 3,
                LinkedDate = "2026-04-18",
                Status = "Active",
                Sla = "97% SLA",
                ResponseTime = "1.8 H",
                CreatedAt = (DateTimeOffset?)null,
                UpdatedAt = (DateTimeOffset?)null
            });
    }
}
