using Aurora.SupplyWok.Platform.Profiles.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Profiles.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
///     Entity Framework model configuration extensions for the Profiles bounded context.
/// </summary>
/// <remarks>
///     <see cref="RestaurantProfile" /> and <see cref="SupplierProfile" /> hold Value Objects
///     (<c>PersonName</c>, <c>StreetAddress</c>, <c>EmailAddress</c>) instead of plain primitive
///     properties, so each one is mapped here as an owned type using <c>OwnsOne</c>.
/// </remarks>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the restaurant profile and supplier profile persistence configuration.
    /// </summary>
    /// <param name="builder">The EF model builder.</param>
    public static void ApplyProfilesConfiguration(this ModelBuilder builder)
    {
        // Restaurant Profiles
        builder.Entity<RestaurantProfile>().ToTable("RestaurantProfiles");
        builder.Entity<RestaurantProfile>().HasKey(profile => profile.Id);
        builder.Entity<RestaurantProfile>().Property(profile => profile.Id).ValueGeneratedOnAdd();
        builder.Entity<RestaurantProfile>().Property(profile => profile.BusinessName).IsRequired().HasMaxLength(100);
        builder.Entity<RestaurantProfile>().Property(profile => profile.Status).IsRequired().HasMaxLength(20);
        builder.Entity<RestaurantProfile>().Property(profile => profile.UserId);

        builder.Entity<RestaurantProfile>().OwnsOne(profile => profile.ContactName, contactName =>
        {
            contactName.Property("RestaurantProfileId").HasColumnName("Id");
            contactName.Property(name => name.FirstName).HasColumnName("ContactFirstName").IsRequired().HasMaxLength(60);
            contactName.Property(name => name.LastName).HasColumnName("ContactLastName").IsRequired().HasMaxLength(60);
            contactName.HasData(
                new { RestaurantProfileId = 1, FirstName = "Wei", LastName = "Wang" },
                new { RestaurantProfileId = 2, FirstName = "Mei", LastName = "Chen" },
                new { RestaurantProfileId = 3, FirstName = "Ana", LastName = "Liu" },
                new { RestaurantProfileId = 4, FirstName = "Luis", LastName = "Wong" });
        });

        builder.Entity<RestaurantProfile>().OwnsOne(profile => profile.Address, address =>
        {
            address.Property("RestaurantProfileId").HasColumnName("Id");
            address.Property(a => a.Street).HasColumnName("Street").IsRequired().HasMaxLength(120);
            address.Property(a => a.District).HasColumnName("District").IsRequired().HasMaxLength(80);
            address.Property(a => a.City).HasColumnName("City").IsRequired().HasMaxLength(80);
            address.Property(a => a.Country).HasColumnName("Country").IsRequired().HasMaxLength(80);
            address.HasData(
                new { RestaurantProfileId = 1, Street = "Av. La Marina 456", District = "San Miguel", City = "Lima", Country = "Peru" },
                new { RestaurantProfileId = 2, Street = "Av. Pardo 180", District = "Miraflores", City = "Lima", Country = "Peru" },
                new { RestaurantProfileId = 3, Street = "Calle Las Begonias 321", District = "San Isidro", City = "Lima", Country = "Peru" },
                new { RestaurantProfileId = 4, Street = "Av. Bolivar 910", District = "Pueblo Libre", City = "Lima", Country = "Peru" });
        });

        builder.Entity<RestaurantProfile>().OwnsOne(profile => profile.ContactEmail, email =>
        {
            email.Property("RestaurantProfileId").HasColumnName("Id");
            email.Property(e => e.Address).HasColumnName("ContactEmail").IsRequired().HasMaxLength(150);
            email.HasData(
                new { RestaurantProfileId = 1, Address = "admin@grandragon.pe" },
                new { RestaurantProfileId = 2, Address = "ops@jadeexpress.pe" },
                new { RestaurantProfileId = 3, Address = "contacto@pekinlounge.pe" },
                new { RestaurantProfileId = 4, Address = "gerencia@minggarden.pe" });
        });

        // Supplier Profiles
        builder.Entity<SupplierProfile>().ToTable("SupplierProfiles");
        builder.Entity<SupplierProfile>().HasKey(profile => profile.Id);
        builder.Entity<SupplierProfile>().Property(profile => profile.Id).ValueGeneratedOnAdd();
        builder.Entity<SupplierProfile>().Property(profile => profile.BusinessName).IsRequired().HasMaxLength(100);
        builder.Entity<SupplierProfile>().Property(profile => profile.Phone).IsRequired().HasMaxLength(30);
        builder.Entity<SupplierProfile>().Property(profile => profile.Category).IsRequired().HasMaxLength(80);
        builder.Entity<SupplierProfile>().Property(profile => profile.Status).IsRequired().HasMaxLength(20);
        builder.Entity<SupplierProfile>().Property(profile => profile.UserId);

        builder.Entity<SupplierProfile>().OwnsOne(profile => profile.ContactName, contactName =>
        {
            contactName.Property("SupplierProfileId").HasColumnName("Id");
            contactName.Property(name => name.FirstName).HasColumnName("ContactFirstName").IsRequired().HasMaxLength(60);
            contactName.Property(name => name.LastName).HasColumnName("ContactLastName").IsRequired().HasMaxLength(60);
            contactName.HasData(
                new { SupplierProfileId = 201, FirstName = "Mariela", LastName = "Soto" },
                new { SupplierProfileId = 202, FirstName = "Luis", LastName = "Cardenas" },
                new { SupplierProfileId = 203, FirstName = "Zhen", LastName = "Liu" });
        });

        builder.Entity<SupplierProfile>().OwnsOne(profile => profile.Address, address =>
        {
            address.Property("SupplierProfileId").HasColumnName("Id");
            address.Property(a => a.Street).HasColumnName("Street").IsRequired().HasMaxLength(120);
            address.Property(a => a.District).HasColumnName("District").IsRequired().HasMaxLength(80);
            address.Property(a => a.City).HasColumnName("City").IsRequired().HasMaxLength(80);
            address.Property(a => a.Country).HasColumnName("Country").IsRequired().HasMaxLength(80);
            address.HasData(
                new { SupplierProfileId = 201, Street = "Av. Los Olivos 123", District = "San Miguel", City = "Lima", Country = "Peru" },
                new { SupplierProfileId = 202, Street = "Av. Industrial 220", District = "Callao", City = "Lima", Country = "Peru" },
                new { SupplierProfileId = 203, Street = "Jr. Comercio 850", District = "La Victoria", City = "Lima", Country = "Peru" });
        });

        builder.Entity<SupplierProfile>().OwnsOne(profile => profile.ContactEmail, email =>
        {
            email.Property("SupplierProfileId").HasColumnName("Id");
            email.Property(e => e.Address).HasColumnName("ContactEmail").IsRequired().HasMaxLength(150);
            email.HasData(
                new { SupplierProfileId = 201, Address = "msoto@goldenwok.pe" },
                new { SupplierProfileId = 202, Address = "lcardenas@andescold.pe" },
                new { SupplierProfileId = 203, Address = "zliu@orientpantry.pe" });
        });

        SeedProfiles(builder);
    }

    private static void SeedProfiles(ModelBuilder builder)
    {
        builder.Entity<RestaurantProfile>().HasData(
            new
            {
                Id = 1,
                BusinessName = "Gran Dragon Chifa",
                Status = "Active",
                UserId = (int?)null,
                CreatedAt = (DateTimeOffset?)null,
                UpdatedAt = (DateTimeOffset?)null
            },
            new
            {
                Id = 2,
                BusinessName = "Jade Express",
                Status = "Active",
                UserId = (int?)null,
                CreatedAt = (DateTimeOffset?)null,
                UpdatedAt = (DateTimeOffset?)null
            },
            new
            {
                Id = 3,
                BusinessName = "Pekin Lounge",
                Status = "Active",
                UserId = (int?)null,
                CreatedAt = (DateTimeOffset?)null,
                UpdatedAt = (DateTimeOffset?)null
            },
            new
            {
                Id = 4,
                BusinessName = "Ming Garden",
                Status = "Active",
                UserId = (int?)null,
                CreatedAt = (DateTimeOffset?)null,
                UpdatedAt = (DateTimeOffset?)null
            });

        builder.Entity<SupplierProfile>().HasData(
            new
            {
                Id = 201,
                BusinessName = "Golden Wok Produce",
                Phone = "+51 999 111 222",
                Category = "Grains and pantry",
                Status = "Active",
                UserId = (int?)null,
                CreatedAt = (DateTimeOffset?)null,
                UpdatedAt = (DateTimeOffset?)null
            },
            new
            {
                Id = 202,
                BusinessName = "Andes Cold Chain",
                Phone = "+51 999 333 444",
                Category = "Cold products",
                Status = "Active",
                UserId = (int?)null,
                CreatedAt = (DateTimeOffset?)null,
                UpdatedAt = (DateTimeOffset?)null
            },
            new
            {
                Id = 203,
                BusinessName = "Orient Pantry Co.",
                Phone = "+51 999 555 666",
                Category = "Asian sauces and oils",
                Status = "Active",
                UserId = (int?)null,
                CreatedAt = (DateTimeOffset?)null,
                UpdatedAt = (DateTimeOffset?)null
            });
    }
}
