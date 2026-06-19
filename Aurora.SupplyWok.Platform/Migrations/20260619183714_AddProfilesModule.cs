using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Aurora.SupplyWok.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddProfilesModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "restaurant_profiles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    business_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    contact_first_name = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    contact_last_name = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    street = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    district = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    city = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    country = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    contact_email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_restaurant_profiles", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "supplier_profiles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    business_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    contact_first_name = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    contact_last_name = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    street = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    district = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    city = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    country = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    contact_email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_supplier_profiles", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "restaurant_profiles");

            migrationBuilder.DropTable(
                name: "supplier_profiles");
        }
    }
}
