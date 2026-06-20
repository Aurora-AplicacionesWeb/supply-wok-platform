using System.Text.Json;
using Aurora.SupplyWok.Platform.Analytics.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Analytics.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Analytics.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public static void ApplyAnalyticsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<RestaurantAnalytics>(entity =>
        {
            entity.ToTable("RestaurantAnalytics");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.InventoryTrend)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, JsonSerializerOptions),
                    v => JsonSerializer.Deserialize<TrendData>(v, JsonSerializerOptions) ?? new TrendData())
                .HasColumnType("longtext");

            entity.Property(e => e.WeeklyConsumption)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, JsonSerializerOptions),
                    v => JsonSerializer.Deserialize<TrendData>(v, JsonSerializerOptions) ?? new TrendData())
                .HasColumnType("longtext");

            entity.Property(e => e.TemperatureFluctuations)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, JsonSerializerOptions),
                    v => JsonSerializer.Deserialize<TrendData>(v, JsonSerializerOptions) ?? new TrendData())
                .HasColumnType("longtext");

            entity.Property(e => e.TopSuppliersOrders)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, JsonSerializerOptions),
                    v => JsonSerializer.Deserialize<List<TopSupplierOrder>>(v, JsonSerializerOptions) ?? new List<TopSupplierOrder>())
                .HasColumnType("longtext");

            // Seed Data
            entity.HasData(new
            {
                Id = 1,
                InventoryTrend = new TrendData(
                    new List<string> { "Jan", "Feb", "Mar", "Apr", "May", "Jun" },
                    new List<int> { 42, 46, 34, 31, 58, 64 }
                ),
                WeeklyConsumption = new TrendData(
                    new List<string> { "W1", "W2", "W3", "W4", "W5", "W6" },
                    new List<int> { 62, 88, 48, 110, 76, 118 }
                ),
                TemperatureFluctuations = new TrendData(
                    new List<string> { "M", "T", "W", "T", "F", "S", "S" },
                    new List<int> { 22, 10, 46, 28, 66, 18, 10 }
                ),
                TopSuppliersOrders = (ICollection<TopSupplierOrder>)new List<TopSupplierOrder>
                {
                    new("Golden Wok", 85),
                    new("Andes", 60),
                    new("Orient", 45),
                    new("Pacific", 30)
                },
                CreatedAt = new DateTimeOffset(2026, 6, 20, 0, 0, 0, TimeSpan.Zero),
                UpdatedAt = (DateTimeOffset?)null
            });
        });

        builder.Entity<SupplierAnalytics>(entity =>
        {
            entity.ToTable("SupplierAnalytics");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.Aggregate)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, JsonSerializerOptions),
                    v => JsonSerializer.Deserialize<List<SupplierAggregatePeriod>>(v, JsonSerializerOptions) ?? new List<SupplierAggregatePeriod>())
                .HasColumnType("longtext");

            entity.Property(e => e.Clients)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, JsonSerializerOptions),
                    v => JsonSerializer.Deserialize<List<SupplierClientDemand>>(v, JsonSerializerOptions) ?? new List<SupplierClientDemand>())
                .HasColumnType("longtext");

            // Seed Data
            entity.HasData(new
            {
                Id = 1,
                Aggregate = (ICollection<SupplierAggregatePeriod>)new List<SupplierAggregatePeriod>
                {
                    new("May", 240),
                    new("Jun", 260),
                    new("Jul", 272),
                    new("Aug", 288),
                    new("Sep", 304),
                    new("Oct", 319)
                },
                Clients = (ICollection<SupplierClientDemand>)new List<SupplierClientDemand>
                {
                    new(1, "Gran Dragon Chifa", 72, "upward", "Seafood and greens demand trending higher before weekends."),
                    new(2, "Jade Express", 68, "stable", "Keep standard restock cadence; lunch traffic is stable."),
                    new(3, "Pekin Lounge", 55, "watch", "Demand remains moderate with steady weekly orders."),
                    new(4, "Ming Garden", 49, "stable", "Projected demand is steady with no urgent restock signal.")
                },
                CreatedAt = new DateTimeOffset(2026, 6, 20, 0, 0, 0, TimeSpan.Zero),
                UpdatedAt = (DateTimeOffset?)null
            });
        });
    }
}
