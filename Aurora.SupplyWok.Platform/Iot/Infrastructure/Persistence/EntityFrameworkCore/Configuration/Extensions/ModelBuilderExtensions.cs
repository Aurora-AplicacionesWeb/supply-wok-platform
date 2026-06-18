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

        builder.Entity<Sensor>().HasData(
            new
            {
                Id = 301,
                Name = "Main Inventory Weight Sensor",
                MinValue = 0d,
                MaxValue = 10000d,
                Enabled = true,
                LastValue = 850d,
                SensorType = Domain.Model.ValueObjects.ESensorType.Weight,
                CreatedAt = new DateTimeOffset(2026, 6, 1, 0, 0, 0, TimeSpan.Zero),
                UpdatedAt = (DateTimeOffset?)null
            },
            new
            {
                Id = 302,
                Name = "Cold Storage Temperature Sensor",
                MinValue = -10d,
                MaxValue = 8d,
                Enabled = true,
                LastValue = 4d,
                SensorType = Domain.Model.ValueObjects.ESensorType.Temperature,
                CreatedAt = new DateTimeOffset(2026, 6, 1, 0, 0, 0, TimeSpan.Zero),
                UpdatedAt = (DateTimeOffset?)null
            });
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

        builder.Entity<AlertRestaurant>().HasData(
            new
            {
                Id = 301,
                Severity = Domain.Model.ValueObjects.EAlertSeverity.Medium,
                Detail = "Inventory stock differs from main inventory weight sensor.",
                Date = new DateTimeOffset(2026, 6, 1, 8, 0, 0, TimeSpan.Zero),
                Status = Domain.Model.ValueObjects.EAlertStatus.Pending,
                SensorId = 301,
                CreatedAt = new DateTimeOffset(2026, 6, 1, 8, 0, 0, TimeSpan.Zero),
                UpdatedAt = (DateTimeOffset?)null
            },
            new
            {
                Id = 302,
                Severity = Domain.Model.ValueObjects.EAlertSeverity.High,
                Detail = "Cold storage temperature is outside the expected range.",
                Date = new DateTimeOffset(2026, 6, 1, 9, 0, 0, TimeSpan.Zero),
                Status = Domain.Model.ValueObjects.EAlertStatus.Pending,
                SensorId = 302,
                CreatedAt = new DateTimeOffset(2026, 6, 1, 9, 0, 0, TimeSpan.Zero),
                UpdatedAt = (DateTimeOffset?)null
            });

        builder.Entity<AlertSupplier>().HasData(new
        {
            Id = 303,
            Severity = Domain.Model.ValueObjects.EAlertSeverity.Low,
            Detail = "Supplier delivery status should be reviewed.",
            Date = new DateTimeOffset(2026, 6, 1, 10, 0, 0, TimeSpan.Zero),
            Status = Domain.Model.ValueObjects.EAlertStatus.Pending,
            CreatedAt = new DateTimeOffset(2026, 6, 1, 10, 0, 0, TimeSpan.Zero),
            UpdatedAt = (DateTimeOffset?)null
        });
    }
}
