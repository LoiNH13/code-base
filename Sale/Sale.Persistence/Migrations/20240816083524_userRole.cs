using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sale.Persistence.Migrations;

/// <inheritdoc />
public partial class userRole : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "categories",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "text", nullable: false),
                odoo_ref = table.Column<int>(type: "integer", nullable: true),
                weight = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                deleted_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_categories", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "entity_changes",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                change_type = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                entity_id = table.Column<Guid>(type: "uuid", nullable: false),
                entity_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                route = table.Column<string>(type: "text", nullable: true),
                client_ip_address = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                request_id = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                create_by = table.Column<Guid>(type: "uuid", nullable: true),
                created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_entity_changes", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "users",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                managed_by_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                role = table.Column<int>(type: "integer", nullable: false),
                created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                deleted_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                password_hash = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_users", x => x.id);
                table.ForeignKey(
                    name: "FK_users_users_managed_by_user_id",
                    column: x => x.managed_by_user_id,
                    principalTable: "users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "entity_property_changes",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                entity_change_id = table.Column<Guid>(type: "uuid", nullable: false),
                property_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                property_type = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                original_value = table.Column<string>(type: "jsonb", nullable: true),
                new_value = table.Column<string>(type: "jsonb", nullable: true),
                created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_entity_property_changes", x => x.id);
                table.ForeignKey(
                    name: "FK_entity_property_changes_entity_changes_entity_change_id",
                    column: x => x.entity_change_id,
                    principalTable: "entity_changes",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "customers",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                odoo_ref = table.Column<int>(type: "integer", nullable: false),
                name = table.Column<string>(type: "text", nullable: false),
                managed_by_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                visit_per_month = table.Column<int>(type: "integer", nullable: false),
                created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                deleted_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_customers", x => x.id);
                table.ForeignKey(
                    name: "FK_customers_users_managed_by_user_id",
                    column: x => x.managed_by_user_id,
                    principalTable: "users",
                    principalColumn: "id");
            });

        migrationBuilder.CreateTable(
            name: "monthly_reports",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                from_time_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                to_time_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                daily_visitors = table.Column<int>(type: "integer", nullable: false),
                daily_purchases = table.Column<int>(type: "integer", nullable: false),
                online_purchase_rate = table.Column<double>(type: "double precision", nullable: false),
                note = table.Column<string>(type: "text", nullable: true),
                is_confirmed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                create_by_user = table.Column<Guid>(type: "uuid", nullable: false),
                modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                deleted_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_monthly_reports", x => x.id);
                table.ForeignKey(
                    name: "FK_monthly_reports_customers_customer_id",
                    column: x => x.customer_id,
                    principalTable: "customers",
                    principalColumn: "id");
                table.ForeignKey(
                    name: "FK_monthly_reports_users_create_by_user",
                    column: x => x.create_by_user,
                    principalTable: "users",
                    principalColumn: "id");
            });

        migrationBuilder.CreateTable(
            name: "monthly_report_items",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                monthly_report_id = table.Column<Guid>(type: "uuid", nullable: false),
                category_id = table.Column<Guid>(type: "uuid", nullable: false),
                quantity = table.Column<double>(type: "double precision", nullable: false),
                revenue = table.Column<double>(type: "double precision", nullable: false),
                note = table.Column<string>(type: "text", nullable: true),
                created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_monthly_report_items", x => x.id);
                table.ForeignKey(
                    name: "FK_monthly_report_items_monthly_reports_monthly_report_id",
                    column: x => x.monthly_report_id,
                    principalTable: "monthly_reports",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_customers_managed_by_user_id",
            table: "customers",
            column: "managed_by_user_id");

        migrationBuilder.CreateIndex(
            name: "IX_entity_changes_entity_id",
            table: "entity_changes",
            column: "entity_id");

        migrationBuilder.CreateIndex(
            name: "IX_entity_property_changes_entity_change_id",
            table: "entity_property_changes",
            column: "entity_change_id");

        migrationBuilder.CreateIndex(
            name: "IX_monthly_report_items_monthly_report_id",
            table: "monthly_report_items",
            column: "monthly_report_id");

        migrationBuilder.CreateIndex(
            name: "IX_monthly_reports_create_by_user",
            table: "monthly_reports",
            column: "create_by_user");

        migrationBuilder.CreateIndex(
            name: "IX_monthly_reports_customer_id",
            table: "monthly_reports",
            column: "customer_id");

        migrationBuilder.CreateIndex(
            name: "IX_users_email",
            table: "users",
            column: "email");

        migrationBuilder.CreateIndex(
            name: "IX_users_managed_by_user_id",
            table: "users",
            column: "managed_by_user_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "categories");

        migrationBuilder.DropTable(
            name: "entity_property_changes");

        migrationBuilder.DropTable(
            name: "monthly_report_items");

        migrationBuilder.DropTable(
            name: "entity_changes");

        migrationBuilder.DropTable(
            name: "monthly_reports");

        migrationBuilder.DropTable(
            name: "customers");

        migrationBuilder.DropTable(
            name: "users");
    }
}
