using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aurora.SupplyWok.Platform.Migrations
{
    /// <inheritdoc />
    public partial class MoveSupplierRestaurantIdentityToProfiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "f_k_catalog_items__suppliers_supplier_id",
                table: "catalog_items");

            migrationBuilder.AddColumn<string>(
                name: "category",
                table: "supplier_profiles",
                type: "varchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "phone",
                table: "supplier_profiles",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "supplier_restaurants",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    supplier_profile_id = table.Column<int>(type: "int", nullable: false),
                    restaurant_profile_id = table.Column<int>(type: "int", nullable: false),
                    linked_date = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    sla = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    response_time = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_supplier_restaurants", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    email = table.Column<string>(type: "longtext", nullable: false),
                    password_hash = table.Column<string>(type: "longtext", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_users", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.Sql("""
                UPDATE supplier_profiles sp
                JOIN suppliers s ON s.id = sp.id
                SET sp.business_name = COALESCE(NULLIF(sp.business_name, ''), s.name),
                    sp.category = COALESCE(NULLIF(sp.category, ''), s.category),
                    sp.phone = COALESCE(NULLIF(sp.phone, ''), s.phone),
                    sp.contact_email = COALESCE(NULLIF(sp.contact_email, ''), s.email),
                    sp.contact_first_name = COALESCE(NULLIF(sp.contact_first_name, ''), LEFT(SUBSTRING_INDEX(TRIM(s.contact_name), ' ', 1), 60)),
                    sp.contact_last_name = COALESCE(NULLIF(sp.contact_last_name, ''),
                        LEFT(CASE
                            WHEN LOCATE(' ', TRIM(s.contact_name)) > 0
                                THEN TRIM(SUBSTRING(TRIM(s.contact_name), LOCATE(' ', TRIM(s.contact_name)) + 1))
                            ELSE 'Contact'
                        END, 60));
                """);

            migrationBuilder.Sql("""
                INSERT INTO supplier_profiles
                    (id, business_name, category, phone, status, user_id, created_at, updated_at,
                     street, district, city, country, contact_email, contact_first_name, contact_last_name)
                SELECT s.id,
                       s.name,
                       s.category,
                       s.phone,
                       'Active',
                       NULL,
                       s.created_at,
                       s.updated_at,
                       'Pending address',
                       'Pending district',
                       'Lima',
                       'Peru',
                       s.email,
                       LEFT(SUBSTRING_INDEX(TRIM(s.contact_name), ' ', 1), 60),
                       LEFT(CASE
                           WHEN LOCATE(' ', TRIM(s.contact_name)) > 0
                               THEN TRIM(SUBSTRING(TRIM(s.contact_name), LOCATE(' ', TRIM(s.contact_name)) + 1))
                           ELSE 'Contact'
                       END, 60)
                FROM suppliers s
                WHERE NOT EXISTS (
                    SELECT 1
                    FROM supplier_profiles sp
                    WHERE sp.id = s.id
                );
                """);

            migrationBuilder.Sql("""
                UPDATE restaurant_profiles rp
                JOIN clients c ON c.id = rp.id
                SET rp.business_name = COALESCE(NULLIF(rp.business_name, ''), c.name),
                    rp.status = COALESCE(NULLIF(rp.status, ''), c.status),
                    rp.district = COALESCE(NULLIF(rp.district, ''), c.district);
                """);

            migrationBuilder.Sql("""
                INSERT INTO restaurant_profiles
                    (id, business_name, status, user_id, created_at, updated_at,
                     street, district, city, country, contact_email, contact_first_name, contact_last_name)
                SELECT c.id,
                       c.name,
                       c.status,
                       NULL,
                       c.created_at,
                       c.updated_at,
                       'Pending address',
                       c.district,
                       'Lima',
                       'Peru',
                       CONCAT('restaurant-', c.id, '@profiles.local'),
                       LEFT(c.name, 60),
                       'Contact'
                FROM clients c
                WHERE NOT EXISTS (
                    SELECT 1
                    FROM restaurant_profiles rp
                    WHERE rp.id = c.id
                );
                """);

            migrationBuilder.Sql("""
                INSERT INTO supplier_restaurants
                    (id, supplier_profile_id, restaurant_profile_id, linked_date, status, sla, response_time, created_at, updated_at)
                SELECT sc.id,
                       sc.supplier_id,
                       sc.client_id,
                       COALESCE(NULLIF(s.linked_date, ''), '2026-04-21'),
                       'Active',
                       COALESCE(NULLIF(s.sla, ''), 'N/A'),
                       COALESCE(NULLIF(s.response_time, ''), 'N/A'),
                       sc.created_at,
                       sc.updated_at
                FROM supplier_clients sc
                JOIN suppliers s ON s.id = sc.supplier_id
                WHERE EXISTS (
                    SELECT 1 FROM supplier_profiles sp WHERE sp.id = sc.supplier_id
                )
                AND EXISTS (
                    SELECT 1 FROM restaurant_profiles rp WHERE rp.id = sc.client_id
                );
                """);

            migrationBuilder.Sql("""
                INSERT INTO restaurant_profiles
                    (id, business_name, status, user_id, created_at, updated_at,
                     street, district, city, country, contact_email, contact_first_name, contact_last_name)
                SELECT seed.id, seed.business_name, seed.status, NULL, NULL, NULL,
                       seed.street, seed.district, 'Lima', 'Peru', seed.email, seed.first_name, seed.last_name
                FROM (
                    SELECT 1 id, 'Gran Dragon Chifa' business_name, 'Active' status, 'Av. La Marina 456' street,
                           'San Miguel' district, 'admin@grandragon.pe' email, 'Wei' first_name, 'Wang' last_name
                    UNION ALL SELECT 2, 'Jade Express', 'Active', 'Av. Pardo 180',
                           'Miraflores', 'ops@jadeexpress.pe', 'Mei', 'Chen'
                    UNION ALL SELECT 3, 'Pekin Lounge', 'Active', 'Calle Las Begonias 321',
                           'San Isidro', 'contacto@pekinlounge.pe', 'Ana', 'Liu'
                    UNION ALL SELECT 4, 'Ming Garden', 'Active', 'Av. Bolivar 910',
                           'Pueblo Libre', 'gerencia@minggarden.pe', 'Luis', 'Wong'
                ) seed
                WHERE NOT EXISTS (
                    SELECT 1 FROM restaurant_profiles rp WHERE rp.id = seed.id
                );
                """);

            migrationBuilder.Sql("""
                INSERT INTO supplier_profiles
                    (id, business_name, category, phone, status, user_id, created_at, updated_at,
                     street, district, city, country, contact_email, contact_first_name, contact_last_name)
                SELECT seed.id, seed.business_name, seed.category, seed.phone, 'Active', NULL, NULL, NULL,
                       seed.street, seed.district, 'Lima', 'Peru', seed.email, seed.first_name, seed.last_name
                FROM (
                    SELECT 201 id, 'Golden Wok Produce' business_name, 'Grains and pantry' category,
                           '+51 999 111 222' phone, 'Av. Los Olivos 123' street, 'San Miguel' district,
                           'msoto@goldenwok.pe' email, 'Mariela' first_name, 'Soto' last_name
                    UNION ALL SELECT 202, 'Andes Cold Chain', 'Cold products',
                           '+51 999 333 444', 'Av. Industrial 220', 'Callao',
                           'lcardenas@andescold.pe', 'Luis', 'Cardenas'
                    UNION ALL SELECT 203, 'Orient Pantry Co.', 'Asian sauces and oils',
                           '+51 999 555 666', 'Jr. Comercio 850', 'La Victoria',
                           'zliu@orientpantry.pe', 'Zhen', 'Liu'
                ) seed
                WHERE NOT EXISTS (
                    SELECT 1 FROM supplier_profiles sp WHERE sp.id = seed.id
                );
                """);

            migrationBuilder.Sql("""
                INSERT INTO supplier_restaurants
                    (id, supplier_profile_id, restaurant_profile_id, linked_date, status, sla, response_time, created_at, updated_at)
                SELECT seed.id, seed.supplier_profile_id, seed.restaurant_profile_id, seed.linked_date,
                       'Active', seed.sla, seed.response_time, NULL, NULL
                FROM (
                    SELECT 1 id, 201 supplier_profile_id, 1 restaurant_profile_id,
                           '2026-04-21' linked_date, '98% SLA' sla, '1.6 H' response_time
                    UNION ALL SELECT 2, 202, 1, '2026-04-20', '95% SLA', '2.1 H'
                    UNION ALL SELECT 3, 203, 2, '2026-04-19', '91% SLA', '2.9 H'
                    UNION ALL SELECT 4, 201, 3, '2026-04-18', '97% SLA', '1.8 H'
                ) seed
                WHERE NOT EXISTS (
                    SELECT 1
                    FROM supplier_restaurants sr
                    WHERE sr.supplier_profile_id = seed.supplier_profile_id
                      AND sr.restaurant_profile_id = seed.restaurant_profile_id
                )
                ;
                """);

            migrationBuilder.DropTable(
                name: "supplier_clients");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "suppliers");

            migrationBuilder.CreateIndex(
                name: "ix_supplier_restaurants_supplier_restaurant",
                table: "supplier_restaurants",
                columns: new[] { "supplier_profile_id", "restaurant_profile_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "supplier_restaurants");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DeleteData(
                table: "restaurant_profiles",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "restaurant_profiles",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "restaurant_profiles",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "restaurant_profiles",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "supplier_profiles",
                keyColumn: "id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "supplier_profiles",
                keyColumn: "id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "supplier_profiles",
                keyColumn: "id",
                keyValue: 203);

            migrationBuilder.DropColumn(
                name: "category",
                table: "supplier_profiles");

            migrationBuilder.DropColumn(
                name: "phone",
                table: "supplier_profiles");

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    district = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_clients", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "suppliers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    category = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    contact_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    linked_date = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    response_time = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    sla = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    uuid = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_suppliers", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "supplier_clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    supplier_id = table.Column<int>(type: "int", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_supplier_clients", x => x.id);
                    table.ForeignKey(
                        name: "f_k_supplier_clients_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_supplier_clients_suppliers_supplier_id",
                        column: x => x.supplier_id,
                        principalTable: "suppliers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "suppliers",
                columns: new[] { "id", "category", "contact_name", "created_at", "email", "linked_date", "name", "phone", "response_time", "sla", "updated_at", "uuid" },
                values: new object[,]
                {
                    { 201, "Grains and pantry", "Mariela Soto", null, "msoto@goldenwok.pe", "2026-04-21", "Golden Wok Produce", "+51 999 111 222", "1.6 H", "98% SLA", null, new Guid("11111111-1111-1111-1111-111111111201") },
                    { 202, "Cold products", "Luis Cardenas", null, "lcardenas@andescold.pe", "2026-04-20", "Andes Cold Chain", "+51 999 333 444", "2.1 H", "95% SLA", null, new Guid("11111111-1111-1111-1111-111111111202") },
                    { 203, "Asian sauces and oils", "Zhen Liu", null, "zliu@orientpantry.pe", "2026-04-19", "Orient Pantry Co.", "+51 999 555 666", "2.9 H", "91% SLA", null, new Guid("11111111-1111-1111-1111-111111111203") }
                });

            migrationBuilder.CreateIndex(
                name: "i_x_supplier_clients_client_id",
                table: "supplier_clients",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "i_x_supplier_clients_supplier_id_client_id",
                table: "supplier_clients",
                columns: new[] { "supplier_id", "client_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_suppliers_uuid",
                table: "suppliers",
                column: "uuid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "f_k_catalog_items__suppliers_supplier_id",
                table: "catalog_items",
                column: "supplier_id",
                principalTable: "suppliers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
