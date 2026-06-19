using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aurora.SupplyWok.Platform.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    district = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_clients", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dish_categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    order = table.Column<int>(type: "int", nullable: false),
                    active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_dish_categories", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "kitchen_orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    number = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    table_id = table.Column<int>(type: "int", nullable: false),
                    type_service = table.Column<string>(type: "longtext", unicode: false, nullable: false),
                    status = table.Column<string>(type: "longtext", unicode: false, maxLength: 30, nullable: false),
                    observations = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    date_created = table.Column<DateOnly>(type: "date", nullable: false),
                    hour_ready = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    hour_delivered = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    preparation_time = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_kitchen_orders", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "purchase_orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    supplier_id = table.Column<int>(type: "int", nullable: false),
                    supplier_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    restaurant_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    order_date = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    estimated_date = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    priority = table.Column<string>(type: "longtext", unicode: false, nullable: false),
                    status = table.Column<string>(type: "longtext", unicode: false, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_purchase_orders", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sensors",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    min_value = table.Column<double>(type: "double", nullable: false),
                    max_value = table.Column<double>(type: "double", nullable: false),
                    enabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    last_value = table.Column<double>(type: "double", nullable: false),
                    sensor_type = table.Column<string>(type: "longtext", unicode: false, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_sensors", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "suppliers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    uuid = table.Column<Guid>(type: "char(36)", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    contact_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    category = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    linked_date = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    sla = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    response_time = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_suppliers", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "supplies",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    unit_of_measure = table.Column<string>(type: "longtext", unicode: false, nullable: false),
                    current_stock = table.Column<int>(type: "int", nullable: false),
                    minimum_stock_level = table.Column<int>(type: "int", nullable: false),
                    category = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_supplies", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tables",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    number = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    capacity = table.Column<int>(type: "int", nullable: false),
                    location = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    state = table.Column<string>(type: "longtext", unicode: false, nullable: false),
                    active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_tables", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dishes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    price = table.Column<double>(type: "double", nullable: false),
                    active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    outstanding = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    dish_category_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_dishes", x => x.id);
                    table.ForeignKey(
                        name: "f_k_dishes__dish_categories_dish_category_id",
                        column: x => x.dish_category_id,
                        principalTable: "dish_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "kitchen_order_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    kitchen_order_id = table.Column<int>(type: "int", nullable: false),
                    dish_id = table.Column<int>(type: "int", nullable: false),
                    dish_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    unit_price = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_kitchen_order_items", x => x.id);
                    table.ForeignKey(
                        name: "f_k_kitchen_order_items_kitchen_orders_kitchen_order_id",
                        column: x => x.kitchen_order_id,
                        principalTable: "kitchen_orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "purchase_order_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    purchase_order_id = table.Column<int>(type: "int", nullable: false),
                    inventory_item_id = table.Column<int>(type: "int", nullable: true),
                    product_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    quantity = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    unit_price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    unit_type = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_purchase_order_items", x => x.id);
                    table.ForeignKey(
                        name: "f_k_purchase_order_items_purchase_orders_purchase_order_id",
                        column: x => x.purchase_order_id,
                        principalTable: "purchase_orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "alerts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    severity = table.Column<string>(type: "longtext", unicode: false, nullable: false),
                    detail = table.Column<string>(type: "longtext", nullable: false),
                    date = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    status = table.Column<string>(type: "longtext", unicode: false, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    alert_type = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false),
                    sensor_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_alerts", x => x.id);
                    table.ForeignKey(
                        name: "f_k_alerts_sensors_sensor_id",
                        column: x => x.sensor_id,
                        principalTable: "sensors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "catalog_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    supplier_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    category = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    unit = table.Column<string>(type: "longtext", unicode: false, nullable: false),
                    delivery_conditions = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_catalog_items", x => x.id);
                    table.ForeignKey(
                        name: "f_k_catalog_items__suppliers_supplier_id",
                        column: x => x.supplier_id,
                        principalTable: "suppliers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "supplier_clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    supplier_id = table.Column<int>(type: "int", nullable: false),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "inventory_transactions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    supply_id = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<string>(type: "longtext", unicode: false, nullable: false),
                    amount = table.Column<int>(type: "int", nullable: false),
                    transaction_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    reason = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_inventory_transactions", x => x.id);
                    table.ForeignKey(
                        name: "f_k__inventory_transactions__supplies",
                        column: x => x.supply_id,
                        principalTable: "supplies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "inventory_operations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    inventory_transaction_id = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<string>(type: "longtext", unicode: false, nullable: false),
                    amount = table.Column<int>(type: "int", nullable: false),
                    operation_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    notes = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_inventory_operations", x => x.id);
                    table.ForeignKey(
                        name: "f_k__inventory_operations__inventory_transactions",
                        column: x => x.inventory_transaction_id,
                        principalTable: "inventory_transactions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "alerts",
                columns: new[] { "id", "alert_type", "created_at", "date", "detail", "severity", "status", "updated_at" },
                values: new object[] { 303, "Supplier", new DateTimeOffset(new DateTime(2026, 6, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Supplier delivery status should be reviewed.", "Low", "Pending", null });

            migrationBuilder.InsertData(
                table: "purchase_orders",
                columns: new[] { "id", "code", "created_at", "estimated_date", "order_date", "priority", "restaurant_name", "status", "supplier_id", "supplier_name", "updated_at" },
                values: new object[,]
                {
                    { 1, "PO-24021", null, "2026-05-11", "2026-05-10", "High", "Gran Dragon Chifa", "Pending", 201, "Golden Wok Produce", null },
                    { 2, "PO-24022", null, "2026-05-12", "2026-05-09", "Medium", "Gran Dragon Chifa", "Confirmed", 202, "Andes Cold Chain", null },
                    { 3, "PO-24023", null, "2026-05-13", "2026-05-08", "Low", "Gran Dragon Chifa", "InTransit", 203, "Orient Pantry Co.", null }
                });

            migrationBuilder.InsertData(
                table: "sensors",
                columns: new[] { "id", "created_at", "enabled", "last_value", "max_value", "min_value", "name", "sensor_type", "updated_at" },
                values: new object[,]
                {
                    { 301, new DateTimeOffset(new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), true, 850.0, 10000.0, 0.0, "Main Inventory Weight Sensor", "Weight", null },
                    { 302, new DateTimeOffset(new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), true, 4.0, 8.0, -10.0, "Cold Storage Temperature Sensor", "Temperature", null }
                });

            migrationBuilder.InsertData(
                table: "suppliers",
                columns: new[] { "id", "category", "contact_name", "created_at", "email", "linked_date", "name", "phone", "response_time", "sla", "updated_at", "uuid" },
                values: new object[,]
                {
                    { 201, "Grains and pantry", "Mariela Soto", null, "msoto@goldenwok.pe", "2026-04-21", "Golden Wok Produce", "+51 999 111 222", "1.6 H", "98% SLA", null, new Guid("11111111-1111-1111-1111-111111111201") },
                    { 202, "Cold products", "Luis Cardenas", null, "lcardenas@andescold.pe", "2026-04-20", "Andes Cold Chain", "+51 999 333 444", "2.1 H", "95% SLA", null, new Guid("11111111-1111-1111-1111-111111111202") },
                    { 203, "Asian sauces and oils", "Zhen Liu", null, "zliu@orientpantry.pe", "2026-04-19", "Orient Pantry Co.", "+51 999 555 666", "2.9 H", "91% SLA", null, new Guid("11111111-1111-1111-1111-111111111203") }
                });

            migrationBuilder.InsertData(
                table: "alerts",
                columns: new[] { "id", "alert_type", "created_at", "date", "detail", "sensor_id", "severity", "status", "updated_at" },
                values: new object[,]
                {
                    { 301, "Restaurant", new DateTimeOffset(new DateTime(2026, 6, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Inventory stock differs from main inventory weight sensor.", 301, "Medium", "Pending", null },
                    { 302, "Restaurant", new DateTimeOffset(new DateTime(2026, 6, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Cold storage temperature is outside the expected range.", 302, "High", "Pending", null }
                });

            migrationBuilder.InsertData(
                table: "purchase_order_items",
                columns: new[] { "id", "inventory_item_id", "product_name", "purchase_order_id", "quantity", "unit_price", "unit_type" },
                values: new object[,]
                {
                    { 1, 101, "Rice", 1, 25m, 4.5m, "kg" },
                    { 2, 102, "Soy Sauce", 1, 12m, 8.2m, "ltr" },
                    { 3, 103, "Chicken Breast", 2, 18m, 14.8m, "kg" },
                    { 4, 104, "Sesame Oil", 3, 6m, 18.4m, "ltr" }
                });

            migrationBuilder.CreateIndex(
                name: "i_x_alerts_sensor_id",
                table: "alerts",
                column: "sensor_id");

            migrationBuilder.CreateIndex(
                name: "i_x_catalog_items_supplier_id",
                table: "catalog_items",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "i_x_dishes_dish_category_id",
                table: "dishes",
                column: "dish_category_id");

            migrationBuilder.CreateIndex(
                name: "i_x_inventory_operations_inventory_transaction_id",
                table: "inventory_operations",
                column: "inventory_transaction_id");

            migrationBuilder.CreateIndex(
                name: "i_x_inventory_transactions_supply_id",
                table: "inventory_transactions",
                column: "supply_id");

            migrationBuilder.CreateIndex(
                name: "i_x_kitchen_order_items_kitchen_order_id",
                table: "kitchen_order_items",
                column: "kitchen_order_id");

            migrationBuilder.CreateIndex(
                name: "i_x_purchase_order_items_purchase_order_id",
                table: "purchase_order_items",
                column: "purchase_order_id");

            migrationBuilder.CreateIndex(
                name: "i_x_purchase_orders_code",
                table: "purchase_orders",
                column: "code",
                unique: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "alerts");

            migrationBuilder.DropTable(
                name: "catalog_items");

            migrationBuilder.DropTable(
                name: "dishes");

            migrationBuilder.DropTable(
                name: "inventory_operations");

            migrationBuilder.DropTable(
                name: "kitchen_order_items");

            migrationBuilder.DropTable(
                name: "purchase_order_items");

            migrationBuilder.DropTable(
                name: "supplier_clients");

            migrationBuilder.DropTable(
                name: "tables");

            migrationBuilder.DropTable(
                name: "sensors");

            migrationBuilder.DropTable(
                name: "dish_categories");

            migrationBuilder.DropTable(
                name: "inventory_transactions");

            migrationBuilder.DropTable(
                name: "kitchen_orders");

            migrationBuilder.DropTable(
                name: "purchase_orders");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "suppliers");

            migrationBuilder.DropTable(
                name: "supplies");
        }
    }
}
