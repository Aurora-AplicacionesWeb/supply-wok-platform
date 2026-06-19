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
        builder.Entity<Supply>().Property(supply => supply.UnitOfMeasure).IsRequired().HasConversion<string>().HasColumnType("longtext").IsUnicode(false);
        builder.Entity<Supply>().Property(supply => supply.CurrentStock).IsRequired();
        builder.Entity<Supply>().Property(supply => supply.MinimumStockLevel).IsRequired();
        builder.Entity<Supply>().Property(supply => supply.Category).IsRequired().HasMaxLength(80);
        
        // InventoryTransactions
        builder.Entity<InventoryTransaction>().ToTable("InventoryTransactions");
        builder.Entity<InventoryTransaction>().HasKey(t => t.Id);
        builder.Entity<InventoryTransaction>().Property(t => t.Id).ValueGeneratedOnAdd();
        builder.Entity<InventoryTransaction>().Property(t => t.SupplyId).IsRequired();
        builder.Entity<InventoryTransaction>().Property(t => t.Type).IsRequired().HasConversion<string>().HasColumnType("longtext").IsUnicode(false);
        builder.Entity<InventoryTransaction>().Property(t => t.Amount).IsRequired();
        builder.Entity<InventoryTransaction>().Property(t => t.TransactionDate).IsRequired();
        builder.Entity<InventoryTransaction>().Property(t => t.Reason).IsRequired().HasMaxLength(250);
        // Foreign Keys
        builder.Entity<InventoryTransaction>()
            .HasOne(t => t.Supply)
            .WithMany()
            .HasForeignKey(t => t.SupplyId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_InventoryTransactions_Supplies");
        // Composition: Transaction has many Operations
        builder.Entity<InventoryTransaction>()
            .HasMany(t => t.Operations)
            .WithOne(o => o.InventoryTransaction)
            .HasForeignKey(o => o.InventoryTransactionId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_InventoryOperations_InventoryTransactions");
        builder.Entity<InventoryTransaction>()
            .Navigation(t => t.Operations)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
        
        // InventoryOperations
        builder.Entity<InventoryOperation>().ToTable("InventoryOperations");
        builder.Entity<InventoryOperation>().HasKey(o => o.Id);
        builder.Entity<InventoryOperation>().Property(o => o.Id).ValueGeneratedOnAdd();
        builder.Entity<InventoryOperation>().Property(o => o.InventoryTransactionId).IsRequired();
        builder.Entity<InventoryOperation>().Property(o => o.Type).IsRequired().HasConversion<string>().HasColumnType("longtext").IsUnicode(false);
        builder.Entity<InventoryOperation>().Property(o => o.Amount).IsRequired();
        builder.Entity<InventoryOperation>().Property(o => o.OperationDate).IsRequired();
        builder.Entity<InventoryOperation>().Property(o => o.Notes).HasMaxLength(500);
    }
}
