using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aurora.SupplyWok.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddSupplierSettingsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "supplier_settings",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    supplier_profile_id = table.Column<int>(type: "int", nullable: false),
                    supplier_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    support_contact = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    notify_email = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    notify_sms = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    service_zones = table.Column<string>(type: "longtext", nullable: false),
                    contacts = table.Column<string>(type: "longtext", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_supplier_settings", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "supplier_settings",
                columns: new[] { "id", "contacts", "created_at", "notify_email", "notify_sms", "service_zones", "supplier_name", "supplier_profile_id", "support_contact", "updated_at" },
                values: new object[,]
                {
                    { 1, "[{\"name\":\"Mariela Soto\",\"state\":\"online\"}]", null, true, true, "[\"San Miguel\"]", "Golden Wok Produce", 201, "soporte@goldenwok.pe", null },
                    { 2, "[]", null, true, false, "[]", "Andes Cold Chain", 202, "soporte@andescold.pe", null },
                    { 3, "[]", null, true, true, "[]", "Orient Pantry Co.", 203, "soporte@orientpantry.pe", null }
                });

            migrationBuilder.CreateIndex(
                name: "ix_supplier_settings_supplier_profile_id",
                table: "supplier_settings",
                column: "supplier_profile_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "supplier_settings");
        }
    }
}
