using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sale.Persistence.Migrations;

/// <inheritdoc />
public partial class salePlan : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<int>(
            name: "odoo_ref",
            table: "customers",
            type: "integer",
            nullable: true,
            oldClrType: typeof(int),
            oldType: "integer");

        migrationBuilder.AddColumn<bool>(
            name: "is_show_monthly_report",
            table: "categories",
            type: "boolean",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<bool>(
            name: "is_show_sale_plan",
            table: "categories",
            type: "boolean",
            nullable: false,
            defaultValue: false);

        migrationBuilder.CreateTable(
            name: "plan_new_customers",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                code = table.Column<string>(type: "text", nullable: false),
                city_id = table.Column<int>(type: "integer", nullable: false),
                district_id = table.Column<int>(type: "integer", nullable: true),
                ward_id = table.Column<int>(type: "integer", nullable: true),
                is_open = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_plan_new_customers", x => x.id);
                table.ForeignKey(
                    name: "FK_plan_new_customers_customers_customer_id",
                    column: x => x.customer_id,
                    principalTable: "customers",
                    principalColumn: "id");
            });

        migrationBuilder.CreateTable(
            name: "planning_controls",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "text", nullable: false),
                status = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                deleted_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_planning_controls", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "products",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                category_id = table.Column<Guid>(type: "uuid", nullable: true),
                name = table.Column<string>(type: "text", nullable: false),
                odoo_ref = table.Column<int>(type: "integer", nullable: false),
                odoo_code = table.Column<string>(type: "text", nullable: true),
                price = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.0),
                weight = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                inactive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_products", x => x.id);
                table.ForeignKey(
                    name: "FK_products_categories_category_id",
                    column: x => x.category_id,
                    principalTable: "categories",
                    principalColumn: "id");
            });

        migrationBuilder.CreateTable(
            name: "time_frames",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                year = table.Column<int>(type: "integer", nullable: false),
                month = table.Column<int>(type: "integer", nullable: false),
                created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_time_frames", x => x.id);
                table.CheckConstraint("CK_TimeFrame_Month", "month >= 1 AND month <= 12");
                table.CheckConstraint("CK_TimeFrame_Year", "year >= EXTRACT(YEAR FROM CURRENT_DATE)");
            });

        migrationBuilder.CreateTable(
            name: "planning_approvals",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                planning_control_id = table.Column<Guid>(type: "uuid", nullable: false),
                customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                customer_managed_by = table.Column<Guid>(type: "uuid", nullable: false),
                status = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                total_target_amount = table.Column<double>(type: "double precision", nullable: false),
                total_ob_amount = table.Column<double>(type: "double precision", nullable: false),
                status_by_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                deleted_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_planning_approvals", x => x.id);
                table.ForeignKey(
                    name: "FK_planning_approvals_customers_customer_id",
                    column: x => x.customer_id,
                    principalTable: "customers",
                    principalColumn: "id");
                table.ForeignKey(
                    name: "FK_planning_approvals_planning_controls_planning_control_id",
                    column: x => x.planning_control_id,
                    principalTable: "planning_controls",
                    principalColumn: "id");
                table.ForeignKey(
                    name: "FK_planning_approvals_users_status_by_user_id",
                    column: x => x.status_by_user_id,
                    principalTable: "users",
                    principalColumn: "id");
            });

        migrationBuilder.CreateTable(
            name: "customer_time_frames_rel",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                time_frame_id = table.Column<Guid>(type: "uuid", nullable: false),
                created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_customer_time_frames_rel", x => x.id);
                table.ForeignKey(
                    name: "FK_customer_time_frames_rel_customers_customer_id",
                    column: x => x.customer_id,
                    principalTable: "customers",
                    principalColumn: "id");
                table.ForeignKey(
                    name: "FK_customer_time_frames_rel_time_frames_time_frame_id",
                    column: x => x.time_frame_id,
                    principalTable: "time_frames",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "planning_control_lines",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                planning_control_id = table.Column<Guid>(type: "uuid", nullable: false),
                time_frame_id = table.Column<Guid>(type: "uuid", nullable: false),
                is_original_budget = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                is_target = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_planning_control_lines", x => x.id);
                table.ForeignKey(
                    name: "FK_planning_control_lines_planning_controls_planning_control_id",
                    column: x => x.planning_control_id,
                    principalTable: "planning_controls",
                    principalColumn: "id");
                table.ForeignKey(
                    name: "FK_planning_control_lines_time_frames_time_frame_id",
                    column: x => x.time_frame_id,
                    principalTable: "time_frames",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "product_time_frame_prices",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                tfprice_id = table.Column<Guid>(type: "uuid", nullable: false),
                time_frame_id = table.Column<Guid>(type: "uuid", nullable: false),
                price = table.Column<double>(type: "double precision", nullable: false),
                created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_product_time_frame_prices", x => x.id);
                table.ForeignKey(
                    name: "FK_product_time_frame_prices_products_tfprice_id",
                    column: x => x.tfprice_id,
                    principalTable: "products",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_product_time_frame_prices_time_frames_time_frame_id",
                    column: x => x.time_frame_id,
                    principalTable: "time_frames",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "metrics",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                customer_time_frame_id = table.Column<Guid>(type: "uuid", nullable: false),
                product_id = table.Column<Guid>(type: "uuid", nullable: false),
                order_number = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.0),
                order_value = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.0),
                order_ids = table.Column<string>(type: "text", nullable: false),
                return_ids = table.Column<string>(type: "text", nullable: false),
                created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                deleted_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_metrics", x => x.id);
                table.ForeignKey(
                    name: "FK_metrics_customer_time_frames_rel_customer_time_frame_id",
                    column: x => x.customer_time_frame_id,
                    principalTable: "customer_time_frames_rel",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_metrics_products_product_id",
                    column: x => x.product_id,
                    principalTable: "products",
                    principalColumn: "id");
            });

        migrationBuilder.CreateTable(
            name: "forecasts",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                metric_id = table.Column<Guid>(type: "uuid", nullable: false),
                price = table.Column<double>(type: "double precision", nullable: false),
                last_stock_number = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.0),
                whole_sales_number = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.0),
                retail_sales_number = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.0),
                stock_number = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.0),
                created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_forecasts", x => x.id);
                table.ForeignKey(
                    name: "FK_forecasts_metrics_metric_id",
                    column: x => x.metric_id,
                    principalTable: "metrics",
                    principalColumn: "id");
            });

        migrationBuilder.CreateTable(
            name: "original_budgets",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                metric_id = table.Column<Guid>(type: "uuid", nullable: false),
                price = table.Column<double>(type: "double precision", nullable: false),
                last_stock_number = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.0),
                whole_sales_number = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.0),
                retail_sales_number = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.0),
                stock_number = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.0),
                created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_original_budgets", x => x.id);
                table.ForeignKey(
                    name: "FK_original_budgets_metrics_metric_id",
                    column: x => x.metric_id,
                    principalTable: "metrics",
                    principalColumn: "id");
            });

        migrationBuilder.CreateTable(
            name: "targets",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                metric_id = table.Column<Guid>(type: "uuid", nullable: false),
                price = table.Column<double>(type: "double precision", nullable: false),
                last_stock_number = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.0),
                whole_sales_number = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.0),
                retail_sales_number = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.0),
                stock_number = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.0),
                created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_targets", x => x.id);
                table.ForeignKey(
                    name: "FK_targets_metrics_metric_id",
                    column: x => x.metric_id,
                    principalTable: "metrics",
                    principalColumn: "id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_customers_odoo_ref",
            table: "customers",
            column: "odoo_ref",
            unique: true,
            filter: "deleted = false");

        migrationBuilder.CreateIndex(
            name: "IX_customer_time_frames_rel_customer_id_time_frame_id",
            table: "customer_time_frames_rel",
            columns: new[] { "customer_id", "time_frame_id" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_customer_time_frames_rel_time_frame_id",
            table: "customer_time_frames_rel",
            column: "time_frame_id");

        migrationBuilder.CreateIndex(
            name: "IX_forecasts_metric_id",
            table: "forecasts",
            column: "metric_id",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_metrics_customer_time_frame_id_product_id",
            table: "metrics",
            columns: new[] { "customer_time_frame_id", "product_id" },
            unique: true,
            filter: "deleted = false");

        migrationBuilder.CreateIndex(
            name: "IX_metrics_product_id",
            table: "metrics",
            column: "product_id");

        migrationBuilder.CreateIndex(
            name: "IX_original_budgets_metric_id",
            table: "original_budgets",
            column: "metric_id",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_plan_new_customers_code",
            table: "plan_new_customers",
            column: "code",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_plan_new_customers_customer_id",
            table: "plan_new_customers",
            column: "customer_id",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_planning_approvals_customer_id",
            table: "planning_approvals",
            column: "customer_id");

        migrationBuilder.CreateIndex(
            name: "IX_planning_approvals_planning_control_id_customer_id",
            table: "planning_approvals",
            columns: new[] { "planning_control_id", "customer_id" },
            unique: true,
            filter: "deleted = false");

        migrationBuilder.CreateIndex(
            name: "IX_planning_approvals_status_by_user_id",
            table: "planning_approvals",
            column: "status_by_user_id");

        migrationBuilder.CreateIndex(
            name: "IX_planning_control_lines_planning_control_id_time_frame_id",
            table: "planning_control_lines",
            columns: new[] { "planning_control_id", "time_frame_id" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_planning_control_lines_time_frame_id",
            table: "planning_control_lines",
            column: "time_frame_id");

        migrationBuilder.CreateIndex(
            name: "IX_product_time_frame_prices_tfprice_id_time_frame_id",
            table: "product_time_frame_prices",
            columns: new[] { "tfprice_id", "time_frame_id" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_product_time_frame_prices_time_frame_id",
            table: "product_time_frame_prices",
            column: "time_frame_id");

        migrationBuilder.CreateIndex(
            name: "IX_products_category_id",
            table: "products",
            column: "category_id");

        migrationBuilder.CreateIndex(
            name: "IX_products_odoo_ref",
            table: "products",
            column: "odoo_ref",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_targets_metric_id",
            table: "targets",
            column: "metric_id",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_time_frames_year_month",
            table: "time_frames",
            columns: new[] { "year", "month" },
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "forecasts");

        migrationBuilder.DropTable(
            name: "original_budgets");

        migrationBuilder.DropTable(
            name: "plan_new_customers");

        migrationBuilder.DropTable(
            name: "planning_approvals");

        migrationBuilder.DropTable(
            name: "planning_control_lines");

        migrationBuilder.DropTable(
            name: "product_time_frame_prices");

        migrationBuilder.DropTable(
            name: "targets");

        migrationBuilder.DropTable(
            name: "planning_controls");

        migrationBuilder.DropTable(
            name: "metrics");

        migrationBuilder.DropTable(
            name: "customer_time_frames_rel");

        migrationBuilder.DropTable(
            name: "products");

        migrationBuilder.DropTable(
            name: "time_frames");

        migrationBuilder.DropIndex(
            name: "IX_customers_odoo_ref",
            table: "customers");

        migrationBuilder.DropColumn(
            name: "is_show_monthly_report",
            table: "categories");

        migrationBuilder.DropColumn(
            name: "is_show_sale_plan",
            table: "categories");

        migrationBuilder.AlterColumn<int>(
            name: "odoo_ref",
            table: "customers",
            type: "integer",
            nullable: false,
            defaultValue: 0,
            oldClrType: typeof(int),
            oldType: "integer",
            oldNullable: true);
    }
}
