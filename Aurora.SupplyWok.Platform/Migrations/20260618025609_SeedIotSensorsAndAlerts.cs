using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aurora.SupplyWok.Platform.Migrations
{
    /// <inheritdoc />
    public partial class SeedIotSensorsAndAlerts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "alerts",
                columns: new[] { "id", "alert_type", "created_at", "date", "detail", "severity", "status", "updated_at" },
                values: new object[] { 303, "Supplier", new DateTimeOffset(new DateTime(2026, 6, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Supplier delivery status should be reviewed.", "Low", "Pending", null });

            migrationBuilder.InsertData(
                table: "sensors",
                columns: new[] { "id", "created_at", "enabled", "last_value", "max_value", "min_value", "name", "sensor_type", "updated_at" },
                values: new object[,]
                {
                    { 301, new DateTimeOffset(new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), true, 850.0, 10000.0, 0.0, "Main Inventory Weight Sensor", "Weight", null },
                    { 302, new DateTimeOffset(new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), true, 4.0, 8.0, -10.0, "Cold Storage Temperature Sensor", "Temperature", null }
                });

            migrationBuilder.InsertData(
                table: "alerts",
                columns: new[] { "id", "alert_type", "created_at", "date", "detail", "sensor_id", "severity", "status", "updated_at" },
                values: new object[,]
                {
                    { 301, "Restaurant", new DateTimeOffset(new DateTime(2026, 6, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Inventory stock differs from main inventory weight sensor.", 301, "Medium", "Pending", null },
                    { 302, "Restaurant", new DateTimeOffset(new DateTime(2026, 6, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Cold storage temperature is outside the expected range.", 302, "High", "Pending", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "alerts",
                keyColumn: "id",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "alerts",
                keyColumn: "id",
                keyValue: 302);

            migrationBuilder.DeleteData(
                table: "alerts",
                keyColumn: "id",
                keyValue: 303);

            migrationBuilder.DeleteData(
                table: "sensors",
                keyColumn: "id",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "sensors",
                keyColumn: "id",
                keyValue: 302);
        }
    }
}
