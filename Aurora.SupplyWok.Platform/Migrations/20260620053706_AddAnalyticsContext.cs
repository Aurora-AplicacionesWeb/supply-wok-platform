using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Aurora.SupplyWok.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddAnalyticsContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "restaurant_analytics",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    inventory_trend = table.Column<string>(type: "longtext", nullable: false),
                    weekly_consumption = table.Column<string>(type: "longtext", nullable: false),
                    temperature_fluctuations = table.Column<string>(type: "longtext", nullable: false),
                    top_suppliers_orders = table.Column<string>(type: "longtext", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_restaurant_analytics", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "supplier_analytics",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    aggregate = table.Column<string>(type: "longtext", nullable: false),
                    clients = table.Column<string>(type: "longtext", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_supplier_analytics", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "restaurant_analytics",
                columns: new[] { "id", "created_at", "inventory_trend", "temperature_fluctuations", "top_suppliers_orders", "updated_at", "weekly_consumption" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2026, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "{\"labels\":[\"Jan\",\"Feb\",\"Mar\",\"Apr\",\"May\",\"Jun\"],\"data\":[42,46,34,31,58,64]}", "{\"labels\":[\"M\",\"T\",\"W\",\"T\",\"F\",\"S\",\"S\"],\"data\":[22,10,46,28,66,18,10]}", "[{\"supplier\":\"Golden Wok\",\"value\":85},{\"supplier\":\"Andes\",\"value\":60},{\"supplier\":\"Orient\",\"value\":45},{\"supplier\":\"Pacific\",\"value\":30}]", null, "{\"labels\":[\"W1\",\"W2\",\"W3\",\"W4\",\"W5\",\"W6\"],\"data\":[62,88,48,110,76,118]}" });

            migrationBuilder.InsertData(
                table: "supplier_analytics",
                columns: new[] { "id", "aggregate", "clients", "created_at", "updated_at" },
                values: new object[] { 1, "[{\"period\":\"May\",\"value\":240},{\"period\":\"Jun\",\"value\":260},{\"period\":\"Jul\",\"value\":272},{\"period\":\"Aug\",\"value\":288},{\"period\":\"Sep\",\"value\":304},{\"period\":\"Oct\",\"value\":319}]", "[{\"clientId\":1,\"clientName\":\"Gran Dragon Chifa\",\"value\":72,\"trend\":\"upward\",\"summary\":\"Seafood and greens demand trending higher before weekends.\"},{\"clientId\":2,\"clientName\":\"Jade Express\",\"value\":68,\"trend\":\"stable\",\"summary\":\"Keep standard restock cadence; lunch traffic is stable.\"},{\"clientId\":3,\"clientName\":\"Pekin Lounge\",\"value\":55,\"trend\":\"watch\",\"summary\":\"Demand remains moderate with steady weekly orders.\"},{\"clientId\":4,\"clientName\":\"Ming Garden\",\"value\":49,\"trend\":\"stable\",\"summary\":\"Projected demand is steady with no urgent restock signal.\"}]", new DateTimeOffset(new DateTime(2026, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "restaurant_analytics");

            migrationBuilder.DropTable(
                name: "supplier_analytics");
        }
    }
}
