using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public class AppDbContextDesignTimeFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        var basePath = Directory.GetCurrentDirectory();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionStringTemplate = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionStringTemplate))
            throw new InvalidOperationException("Database connection string is not set in the configuration.");

        var connectionString = Environment.ExpandEnvironmentVariables(connectionStringTemplate);
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseMySQL(connectionString);

        return new AppDbContext(optionsBuilder.Options);
    }
}
