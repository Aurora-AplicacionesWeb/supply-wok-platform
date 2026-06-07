using Aurora.SupplyWok.Platform.Iot.Domain.Model.Aggregate;
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
    
}