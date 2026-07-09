using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aurora.SupplyWok.Platform.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
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
                name: "pending_registrations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    public_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    password_hash = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    role = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    plan_code = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    status = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    stripe_checkout_session_id = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: true),
                    stripe_customer_id = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: true),
                    stripe_subscription_id = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: true),
                    business_name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    first_name = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    last_name = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    street = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    district = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    city = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    country = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    contact_email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    phone = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true),
                    category = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: true),
                    expires_at = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_pending_registrations", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "processed_webhook_events",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    stripe_event_id = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    event_type = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    processed_at = table.Column<DateTimeOffset>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_processed_webhook_events", x => x.id);
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
                name: "subscriptions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    role = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    plan_code = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    status = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    stripe_customer_id = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    stripe_subscription_id = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    stripe_price_id = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    current_period_start = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    current_period_end = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_subscriptions", x => x.id);
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
                    phone = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    category = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
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
                    state = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
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
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    email = table.Column<string>(type: "longtext", nullable: false),
                    password_hash = table.Column<string>(type: "longtext", nullable: false),
                    role = table.Column<string>(type: "longtext", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_users", x => x.id);
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
                name: "kitchen_orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    number = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    table_id = table.Column<int>(type: "int", nullable: false),
                    type_service = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    status = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    observations = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    date_created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    hour_ready = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    hour_delivered = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    preparation_time = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_kitchen_orders", x => x.id);
                    table.ForeignKey(
                        name: "f_k_kitchen_orders__tables_table_id",
                        column: x => x.table_id,
                        principalTable: "tables",
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
                    unit_price = table.Column<double>(type: "double", nullable: false),
                    code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    dish_category_id = table.Column<int>(type: "int", nullable: false),
                    active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    outstanding = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
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
                table: "restaurant_profiles",
                columns: new[] { "id", "business_name", "created_at", "status", "updated_at", "user_id", "city", "country", "district", "street", "contact_email", "contact_first_name", "contact_last_name" },
                values: new object[,]
                {
                    { 1, "Gran Dragon Chifa", null, "Active", null, null, "Lima", "Peru", "San Miguel", "Av. La Marina 456", "admin@grandragon.pe", "Wei", "Wang" },
                    { 2, "Jade Express", null, "Active", null, null, "Lima", "Peru", "Miraflores", "Av. Pardo 180", "ops@jadeexpress.pe", "Mei", "Chen" },
                    { 3, "Pekin Lounge", null, "Active", null, null, "Lima", "Peru", "San Isidro", "Calle Las Begonias 321", "contacto@pekinlounge.pe", "Ana", "Liu" },
                    { 4, "Ming Garden", null, "Active", null, null, "Lima", "Peru", "Pueblo Libre", "Av. Bolivar 910", "gerencia@minggarden.pe", "Luis", "Wong" }
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
                table: "supplier_profiles",
                columns: new[] { "id", "business_name", "category", "created_at", "phone", "status", "updated_at", "user_id", "city", "country", "district", "street", "contact_email", "contact_first_name", "contact_last_name" },
                values: new object[,]
                {
                    { 201, "Golden Wok Produce", "Grains and pantry", null, "+51 999 111 222", "Active", null, null, "Lima", "Peru", "San Miguel", "Av. Los Olivos 123", "msoto@goldenwok.pe", "Mariela", "Soto" },
                    { 202, "Andes Cold Chain", "Cold products", null, "+51 999 333 444", "Active", null, null, "Lima", "Peru", "Callao", "Av. Industrial 220", "lcardenas@andescold.pe", "Luis", "Cardenas" },
                    { 203, "Orient Pantry Co.", "Asian sauces and oils", null, "+51 999 555 666", "Active", null, null, "Lima", "Peru", "La Victoria", "Jr. Comercio 850", "zliu@orientpantry.pe", "Zhen", "Liu" }
                });

            migrationBuilder.InsertData(
                table: "supplier_restaurants",
                columns: new[] { "id", "created_at", "linked_date", "response_time", "restaurant_profile_id", "sla", "status", "supplier_profile_id", "updated_at" },
                values: new object[,]
                {
                    { 1, null, "2026-04-21", "1.6 H", 1, "98% SLA", "Active", 201, null },
                    { 2, null, "2026-04-20", "2.1 H", 1, "95% SLA", "Active", 202, null },
                    { 3, null, "2026-04-19", "2.9 H", 2, "91% SLA", "Active", 203, null },
                    { 4, null, "2026-04-18", "1.8 H", 3, "97% SLA", "Active", 201, null }
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
                name: "i_x_kitchen_orders_table_id",
                table: "kitchen_orders",
                column: "table_id");

            migrationBuilder.CreateIndex(
                name: "i_x_pending_registrations_email",
                table: "pending_registrations",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "i_x_pending_registrations_public_id",
                table: "pending_registrations",
                column: "public_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_pending_registrations_stripe_checkout_session_id",
                table: "pending_registrations",
                column: "stripe_checkout_session_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_pending_registrations_stripe_subscription_id",
                table: "pending_registrations",
                column: "stripe_subscription_id");

            migrationBuilder.CreateIndex(
                name: "i_x_processed_webhook_events_stripe_event_id",
                table: "processed_webhook_events",
                column: "stripe_event_id",
                unique: true);

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
                name: "i_x_subscriptions_stripe_subscription_id",
                table: "subscriptions",
                column: "stripe_subscription_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_subscriptions_user_id",
                table: "subscriptions",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_supplier_restaurants_supplier_profile_restaurant_profile",
                table: "supplier_restaurants",
                columns: new[] { "supplier_profile_id", "restaurant_profile_id" },
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
                name: "pending_registrations");

            migrationBuilder.DropTable(
                name: "processed_webhook_events");

            migrationBuilder.DropTable(
                name: "purchase_order_items");

            migrationBuilder.DropTable(
                name: "restaurant_profiles");

            migrationBuilder.DropTable(
                name: "subscriptions");

            migrationBuilder.DropTable(
                name: "supplier_profiles");

            migrationBuilder.DropTable(
                name: "supplier_restaurants");

            migrationBuilder.DropTable(
                name: "users");

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
                name: "supplies");

            migrationBuilder.DropTable(
                name: "tables");
        }
    }
}
