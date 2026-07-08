using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Aurora.SupplyWok.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddSubscriptionsBoundedContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "i_x_subscriptions_stripe_subscription_id",
                table: "subscriptions",
                column: "stripe_subscription_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_subscriptions_user_id",
                table: "subscriptions",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pending_registrations");

            migrationBuilder.DropTable(
                name: "processed_webhook_events");

            migrationBuilder.DropTable(
                name: "subscriptions");
        }
    }
}
