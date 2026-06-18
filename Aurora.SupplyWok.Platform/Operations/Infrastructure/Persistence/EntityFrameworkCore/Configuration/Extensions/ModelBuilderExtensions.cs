using Aurora.SupplyWok.Platform.Operations.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Operations.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyOperationsConfiguration(this ModelBuilder builder)
    {
        // Operations Context
        
        builder.Entity<Table>().ToTable("Tables");
        builder.Entity<Table>().HasKey(t => t.Id);
        builder.Entity<Table>().Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Table>().Property(t => t.Number).IsRequired().HasMaxLength(50);
        builder.Entity<Table>().Property(t => t.Capacity).IsRequired();
        builder.Entity<Table>().Property(t => t.Location).IsRequired().HasMaxLength(50);
        builder.Entity<Table>().Property(t => t.State).IsRequired().HasConversion<string>();
        builder.Entity<Table>().Property(t => t.Active).IsRequired().HasDefaultValue(true);

        // Dishes
        builder.Entity<Dish>().ToTable("Dishes");
        builder.Entity<Dish>().HasKey(d => d.Id);
        builder.Entity<Dish>().Property(d => d.Id).ValueGeneratedOnAdd();
        builder.Entity<Dish>().Property(d => d.Code).IsRequired().HasMaxLength(50);
        builder.Entity<Dish>().Property(d => d.Name).IsRequired().HasMaxLength(100);
        builder.Entity<Dish>().Property(d => d.Quantity).IsRequired();
        builder.Entity<Dish>().Property(d => d.Description).HasMaxLength(500);
        builder.Entity<Dish>().Property(d => d.Price).IsRequired();
        builder.Entity<Dish>().Property(d => d.Active).IsRequired().HasDefaultValue(true);
        builder.Entity<Dish>().Property(d => d.Outstanding).IsRequired().HasDefaultValue(true);

        builder.Entity<Dish>()
            .HasOne(d => d.DishCategory)
            .WithMany()
            .HasForeignKey(d => d.DishCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // DishCategories
        builder.Entity<DishCategory>().ToTable("DishCategories");
        builder.Entity<DishCategory>().HasKey(dc => dc.Id);
        builder.Entity<DishCategory>().Property(dc => dc.Id).ValueGeneratedOnAdd();
        builder.Entity<DishCategory>().Property(dc => dc.Name).IsRequired().HasMaxLength(80);
        builder.Entity<DishCategory>().Property(dc => dc.Order).IsRequired();
        builder.Entity<DishCategory>().Property(dc => dc.Active).IsRequired().HasDefaultValue(true);
    }
}