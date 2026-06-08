using Aurora.SupplyWok.Platform.Operations.Domain.Model.Aggregate;
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
    }
}