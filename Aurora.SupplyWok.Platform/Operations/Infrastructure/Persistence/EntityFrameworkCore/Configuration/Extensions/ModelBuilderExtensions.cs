using Aurora.SupplyWok.Platform.Operations.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Operations.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Operations.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyOperationsConfiguration(this ModelBuilder builder)
    {
        // Operations Context
        
        // Tables
        builder.Entity<Table>().ToTable("Tables");
        builder.Entity<Table>().HasKey(t => t.Id);
        builder.Entity<Table>().Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Table>().Property(t => t.Number).IsRequired().HasMaxLength(50);
        builder.Entity<Table>().Property(t => t.Capacity).IsRequired();
        builder.Entity<Table>().Property(t => t.Location).IsRequired().HasMaxLength(50);
        builder.Entity<Table>().Property(t => t.State).IsRequired().HasConversion<string>().HasColumnType("longtext").IsUnicode(false);
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
        // Foreign Keys
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

        // KitchenOrders
        builder.Entity<KitchenOrder>().ToTable("KitchenOrders");
        builder.Entity<KitchenOrder>().HasKey(ko => ko.Id);
        builder.Entity<KitchenOrder>().Property(ko => ko.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<KitchenOrder>().Property(ko => ko.Number).IsRequired().HasMaxLength(50);
        builder.Entity<KitchenOrder>().Property(ko => ko.TableId).IsRequired();
        builder.Entity<KitchenOrder>().Property(ko => ko.TypeService).IsRequired().HasConversion<string>().HasColumnType("longtext").IsUnicode(false);
        builder.Entity<KitchenOrder>().Property(ko => ko.Status).IsRequired().HasConversion<string>().HasColumnType("longtext").IsUnicode(false).HasMaxLength(30);
        builder.Entity<KitchenOrder>().Property(ko => ko.Observations).HasMaxLength(500);
        builder.Entity<KitchenOrder>().Property(ko => ko.DateCreated).IsRequired();
        builder.Entity<KitchenOrder>().Property(ko => ko.HourReady);
        builder.Entity<KitchenOrder>().Property(ko => ko.HourDelivered);
        builder.Entity<KitchenOrder>().Property(ko => ko.PreparationTime).HasDefaultValue(0);
        builder.Entity<KitchenOrder>().Ignore(ko => ko.TotalPrice);
        // Foreign Keys
        builder.Entity<KitchenOrder>()
            .HasMany(ko => ko.Items)
            .WithOne()
            .HasForeignKey(ki => ki.KitchenOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // KitchenOrderItems
        builder.Entity<KitchenOrderItem>().ToTable("KitchenOrderItems");
        builder.Entity<KitchenOrderItem>().HasKey(ki => ki.Id);
        builder.Entity<KitchenOrderItem>().Property(ki => ki.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<KitchenOrderItem>().Property(ki => ki.DishId).IsRequired();
        builder.Entity<KitchenOrderItem>().Property(ki => ki.DishName).IsRequired().HasMaxLength(100);
        builder.Entity<KitchenOrderItem>().Property(ki => ki.Quantity).IsRequired();
        builder.Entity<KitchenOrderItem>().Property(ki => ki.UnitPrice).IsRequired();
        builder.Entity<KitchenOrderItem>().Ignore(ki => ki.SubTotal);
    }
}