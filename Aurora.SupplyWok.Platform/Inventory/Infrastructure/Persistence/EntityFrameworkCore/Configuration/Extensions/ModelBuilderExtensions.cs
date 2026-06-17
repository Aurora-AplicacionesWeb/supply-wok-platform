using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Inventory.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyInventoryConfiguration(this ModelBuilder builder)
    {
        // Inventory Context
        
        // Supplies
        builder.Entity<Supply>().ToTable("Supplies");
        builder.Entity<Supply>().HasKey(supply => supply.Id);
        builder.Entity<Supply>().Property(supply => supply.Id).ValueGeneratedOnAdd();
        builder.Entity<Supply>().Property(supply => supply.Name).IsRequired().HasMaxLength(100);
        builder.Entity<Supply>().Property(supply => supply.UnitOfMeasure).IsRequired().HasConversion<string>();
        builder.Entity<Supply>().Property(supply => supply.CurrentStock).IsRequired();
        builder.Entity<Supply>().Property(supply => supply.MinimumStockLevel).IsRequired();
        builder.Entity<Supply>().Property(supply => supply.Category).IsRequired().HasMaxLength(80);
        
        // StockMovements
        builder.Entity<StockMovement>().ToTable("StockMovements");
        builder.Entity<StockMovement>().HasKey(movement => movement.Id);
        builder.Entity<StockMovement>().Property(movement => movement.Id).ValueGeneratedOnAdd();
        builder.Entity<StockMovement>().Property(movement => movement.SupplyId).IsRequired();
        builder.Entity<StockMovement>().Property(movement => movement.Type).IsRequired().HasConversion<string>();
        builder.Entity<StockMovement>().Property(movement => movement.Amount).IsRequired();
        builder.Entity<StockMovement>().Property(movement => movement.Date).IsRequired();
        builder.Entity<StockMovement>().Property(movement => movement.Reason).IsRequired().HasMaxLength(250);
        // Foreign Keys
        builder.Entity<StockMovement>()
            .HasOne(movement => movement.Supply)
            .WithMany()
            .HasForeignKey(movement => movement.SupplyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
