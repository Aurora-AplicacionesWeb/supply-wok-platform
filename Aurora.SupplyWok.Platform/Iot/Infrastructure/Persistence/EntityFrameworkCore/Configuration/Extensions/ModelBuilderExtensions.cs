using Aurora.SupplyWok.Platform.Iot.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Iot.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Iot.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplySensorsConfiguration(this ModelBuilder builder)
    {
        // Sensors Context

        builder.Entity<Sensor>().ToTable("Sensors");
        builder.Entity<Sensor>().HasKey(s => s.Id);
        builder.Entity<Sensor>().Property(s => s.Id).ValueGeneratedOnAdd();
        builder.Entity<Sensor>().Property(s => s.Name).IsRequired().HasMaxLength(50);
        builder.Entity<Sensor>().Property(s => s.MinValue).IsRequired();
        builder.Entity<Sensor>().Property(s => s.MaxValue).IsRequired();
        builder.Entity<Sensor>().Property(s => s.Enabled).IsRequired();
        builder.Entity<Sensor>().Property(s => s.LastValue).IsRequired();
        builder.Entity<Sensor>().Property(s => s.SensorType).IsRequired().HasConversion<string>();
    }

    public static void ApplyAlertsConfiguration(this ModelBuilder builder)
    {
        // Alerts Context

        builder.Entity<Alert>().ToTable("Alerts");
        builder.Entity<Alert>().HasKey(a => a.Id);
        builder.Entity<Alert>().Property(a => a.Id).ValueGeneratedOnAdd();
        builder.Entity<Alert>().Property(a => a.Detail).IsRequired();
        builder.Entity<Alert>().Property(a => a.Severity).IsRequired().HasConversion<string>();
        builder.Entity<Alert>().Property(a => a.Date).IsRequired();
        builder.Entity<Alert>().Property(a => a.Status).IsRequired().HasConversion<string>();

        builder.Entity<Alert>()
            .HasDiscriminator<string>("AlertType")
            .HasValue<AlertRestaurant>("Restaurant")
            .HasValue<AlertSupplier>("Supplier");

        // AlertRestaurant subclass configuration
        builder.Entity<AlertRestaurant>()
            .HasOne(ar => ar.Sensor)
            .WithMany()
            .HasForeignKey(ar => ar.SensorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}