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
        });

        builder.Entity<RestaurantProfile>().OwnsOne(profile => profile.Address, address =>
        {
            address.Property("RestaurantProfileId").HasColumnName("Id");
            address.Property(a => a.Street).HasColumnName("Street").IsRequired().HasMaxLength(120);
            address.Property(a => a.District).HasColumnName("District").IsRequired().HasMaxLength(80);
            address.Property(a => a.City).HasColumnName("City").IsRequired().HasMaxLength(80);
            address.Property(a => a.Country).HasColumnName("Country").IsRequired().HasMaxLength(80);
        });

        builder.Entity<RestaurantProfile>().OwnsOne(profile => profile.ContactEmail, email =>
        {
            email.Property("RestaurantProfileId").HasColumnName("Id");
            email.Property(e => e.Address).HasColumnName("ContactEmail").IsRequired().HasMaxLength(150);
        });

        // Supplier Profiles
        builder.Entity<SupplierProfile>().ToTable("SupplierProfiles");
        builder.Entity<SupplierProfile>().HasKey(profile => profile.Id);
        builder.Entity<SupplierProfile>().Property(profile => profile.Id).ValueGeneratedOnAdd();
        builder.Entity<SupplierProfile>().Property(profile => profile.BusinessName).IsRequired().HasMaxLength(100);
        builder.Entity<SupplierProfile>().Property(profile => profile.Status).IsRequired().HasMaxLength(20);
        builder.Entity<SupplierProfile>().Property(profile => profile.UserId);

        builder.Entity<SupplierProfile>().OwnsOne(profile => profile.ContactName, contactName =>
        {
            contactName.Property("SupplierProfileId").HasColumnName("Id");
            contactName.Property(name => name.FirstName).HasColumnName("ContactFirstName").IsRequired().HasMaxLength(60);
            contactName.Property(name => name.LastName).HasColumnName("ContactLastName").IsRequired().HasMaxLength(60);
        });

        builder.Entity<SupplierProfile>().OwnsOne(profile => profile.Address, address =>
        {
            address.Property("SupplierProfileId").HasColumnName("Id");
            address.Property(a => a.Street).HasColumnName("Street").IsRequired().HasMaxLength(120);
            address.Property(a => a.District).HasColumnName("District").IsRequired().HasMaxLength(80);
            address.Property(a => a.City).HasColumnName("City").IsRequired().HasMaxLength(80);
            address.Property(a => a.Country).HasColumnName("Country").IsRequired().HasMaxLength(80);
        });

        builder.Entity<SupplierProfile>().OwnsOne(profile => profile.ContactEmail, email =>
        {
            email.Property("SupplierProfileId").HasColumnName("Id");
            email.Property(e => e.Address).HasColumnName("ContactEmail").IsRequired().HasMaxLength(150);
        });
    }
}
