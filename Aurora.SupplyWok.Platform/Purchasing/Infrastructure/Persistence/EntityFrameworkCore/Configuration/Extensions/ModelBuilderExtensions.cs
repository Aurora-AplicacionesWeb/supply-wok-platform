using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Purchasing.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Purchasing.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyPurchasingConfiguration(this ModelBuilder builder)
    {
        builder.Entity<PurchaseOrder>().ToTable("PurchaseOrders");
        builder.Entity<PurchaseOrder>().HasKey(order => order.Id);
        builder.Entity<PurchaseOrder>().Property(order => order.Id).ValueGeneratedOnAdd();
        builder.Entity<PurchaseOrder>().Property(order => order.Code).IsRequired().HasMaxLength(20);
        builder.Entity<PurchaseOrder>().HasIndex(order => order.Code).IsUnique();
        builder.Entity<PurchaseOrder>().Property(order => order.SupplierId).IsRequired();
        builder.Entity<PurchaseOrder>().Property(order => order.SupplierName).IsRequired().HasMaxLength(100);
        builder.Entity<PurchaseOrder>().Property(order => order.RestaurantName).IsRequired().HasMaxLength(100);
        builder.Entity<PurchaseOrder>().Property(order => order.OrderDate).IsRequired().HasMaxLength(10);
        builder.Entity<PurchaseOrder>().Property(order => order.EstimatedDate).HasMaxLength(10);
        builder.Entity<PurchaseOrder>().Property(order => order.Priority).IsRequired().HasConversion<string>();
        builder.Entity<PurchaseOrder>().Property(order => order.Status).IsRequired().HasConversion<string>();
        builder.Entity<PurchaseOrder>()
            .HasMany(order => order.Items)
            .WithOne()
            .HasForeignKey(item => item.PurchaseOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<PurchaseOrderItem>().ToTable("PurchaseOrderItems");
        builder.Entity<PurchaseOrderItem>().HasKey(item => item.Id);
        builder.Entity<PurchaseOrderItem>().Property(item => item.Id).ValueGeneratedOnAdd();
        builder.Entity<PurchaseOrderItem>().Property(item => item.ProductName).IsRequired().HasMaxLength(100);
        builder.Entity<PurchaseOrderItem>().Property(item => item.Quantity).IsRequired().HasPrecision(18, 2);
        builder.Entity<PurchaseOrderItem>().Property(item => item.UnitPrice).IsRequired().HasPrecision(18, 2);
        builder.Entity<PurchaseOrderItem>().Property(item => item.UnitType).IsRequired().HasMaxLength(20);

        builder.Entity<Supplier>().ToTable("Suppliers");
        builder.Entity<Supplier>().HasKey(supplier => supplier.Id);
        builder.Entity<Supplier>().Property(supplier => supplier.Id).ValueGeneratedNever();
        builder.Entity<Supplier>().Property(supplier => supplier.Name).IsRequired().HasMaxLength(100);
        builder.Entity<Supplier>().Property(supplier => supplier.ContactName).IsRequired().HasMaxLength(100);
        builder.Entity<Supplier>().Property(supplier => supplier.Email).IsRequired().HasMaxLength(100);
        builder.Entity<Supplier>().Property(supplier => supplier.Phone).IsRequired().HasMaxLength(30);
        builder.Entity<Supplier>().Property(supplier => supplier.Category).IsRequired().HasMaxLength(80);
        builder.Entity<Supplier>().Property(supplier => supplier.LinkedDate).IsRequired().HasMaxLength(10);
        builder.Entity<Supplier>().Property(supplier => supplier.Sla).IsRequired().HasMaxLength(20);
        builder.Entity<Supplier>().Property(supplier => supplier.ResponseTime).IsRequired().HasMaxLength(20);

        SeedSuppliers(builder);
        SeedPurchaseOrders(builder);
    }

    private static void SeedSuppliers(ModelBuilder builder)
    {
        builder.Entity<Supplier>().HasData(
            new
            {
                Id = 201,
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

    private static void SeedPurchaseOrders(ModelBuilder builder)
    {
        builder.Entity<PurchaseOrder>().HasData(
            new
            {
                Id = 1,
                Code = "PO-24021",
                SupplierId = 201,
                SupplierName = "Golden Wok Produce",
                RestaurantName = "Gran Dragon Chifa",
                OrderDate = "2026-05-10",
                EstimatedDate = "2026-05-11",
                Priority = EPurchaseOrderPriority.High,
                Status = EPurchaseOrderStatus.Pending,
                CreatedAt = (DateTimeOffset?)null,
                UpdatedAt = (DateTimeOffset?)null
            },
            new
            {
                Id = 2,
                Code = "PO-24022",
                SupplierId = 202,
                SupplierName = "Andes Cold Chain",
                RestaurantName = "Gran Dragon Chifa",
                OrderDate = "2026-05-09",
                EstimatedDate = "2026-05-12",
                Priority = EPurchaseOrderPriority.Medium,
                Status = EPurchaseOrderStatus.Confirmed,
                CreatedAt = (DateTimeOffset?)null,
                UpdatedAt = (DateTimeOffset?)null
            },
            new
            {
                Id = 3,
                Code = "PO-24023",
                SupplierId = 203,
                SupplierName = "Orient Pantry Co.",
                RestaurantName = "Gran Dragon Chifa",
                OrderDate = "2026-05-08",
                EstimatedDate = "2026-05-13",
                Priority = EPurchaseOrderPriority.Low,
                Status = EPurchaseOrderStatus.InTransit,
                CreatedAt = (DateTimeOffset?)null,
                UpdatedAt = (DateTimeOffset?)null
            });

        builder.Entity<PurchaseOrderItem>().HasData(
            new { Id = 1, PurchaseOrderId = 1, InventoryItemId = (int?)101, ProductName = "Rice", Quantity = 25m, UnitPrice = 4.5m, UnitType = "kg" },
            new { Id = 2, PurchaseOrderId = 1, InventoryItemId = (int?)102, ProductName = "Soy Sauce", Quantity = 12m, UnitPrice = 8.2m, UnitType = "ltr" },
            new { Id = 3, PurchaseOrderId = 2, InventoryItemId = (int?)103, ProductName = "Chicken Breast", Quantity = 18m, UnitPrice = 14.8m, UnitType = "kg" },
            new { Id = 4, PurchaseOrderId = 3, InventoryItemId = (int?)104, ProductName = "Sesame Oil", Quantity = 6m, UnitPrice = 18.4m, UnitType = "ltr" });
    }
}
