using System.Text.Json;
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
    }
}
