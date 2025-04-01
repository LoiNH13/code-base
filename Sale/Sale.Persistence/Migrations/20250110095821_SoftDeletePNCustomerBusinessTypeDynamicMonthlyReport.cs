using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sale.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SoftDeletePNCustomerBusinessTypeDynamicMonthlyReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customer_time_frames_rel_customers_customer_id",
                table: "customer_time_frames_rel");

            migrationBuilder.DropForeignKey(
                name: "FK_monthly_report_items_monthly_reports_monthly_report_id",
                table: "monthly_report_items");

            migrationBuilder.DropCheckConstraint(
                name: "CK_TimeFrame_Year",
                table: "time_frames");

            migrationBuilder.DropIndex(
                name: "IX_plan_new_customers_customer_id",
                table: "plan_new_customers");

            migrationBuilder.DropIndex(
                name: "IX_monthly_report_items_monthly_report_id",
                table: "monthly_report_items");

            migrationBuilder.AddColumn<int>(
                name: "business_type",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "plan_new_customers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_on_utc",
                table: "plan_new_customers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "business_type",
                table: "monthly_reports",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "dynamic_data",
                table: "monthly_reports",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "group",
                table: "monthly_report_items",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "business_type",
                table: "customers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddCheckConstraint(
                name: "CK_TimeFrame_Year",
                table: "time_frames",
                sql: "(year)::numeric <= EXTRACT(year FROM CURRENT_DATE) + 1");

            migrationBuilder.CreateIndex(
                name: "IX_plan_new_customers_customer_id",
                table: "plan_new_customers",
                column: "customer_id",
                unique: true,
                filter: "deleted = false");

            migrationBuilder.CreateIndex(
                name: "IX_monthly_report_items_monthly_report_id_group_category_id",
                table: "monthly_report_items",
                columns: new[] { "monthly_report_id", "group", "category_id" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_customer_time_frames_rel_customers_customer_id",
                table: "customer_time_frames_rel",
                column: "customer_id",
                principalTable: "customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_monthly_report_items_monthly_reports_monthly_report_id",
                table: "monthly_report_items",
                column: "monthly_report_id",
                principalTable: "monthly_reports",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customer_time_frames_rel_customers_customer_id",
                table: "customer_time_frames_rel");

            migrationBuilder.DropForeignKey(
                name: "FK_monthly_report_items_monthly_reports_monthly_report_id",
                table: "monthly_report_items");

            migrationBuilder.DropCheckConstraint(
                name: "CK_TimeFrame_Year",
                table: "time_frames");

            migrationBuilder.DropIndex(
                name: "IX_plan_new_customers_customer_id",
                table: "plan_new_customers");

            migrationBuilder.DropIndex(
                name: "IX_monthly_report_items_monthly_report_id_group_category_id",
                table: "monthly_report_items");

            migrationBuilder.DropColumn(
                name: "business_type",
                table: "users");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "plan_new_customers");

            migrationBuilder.DropColumn(
                name: "deleted_on_utc",
                table: "plan_new_customers");

            migrationBuilder.DropColumn(
                name: "business_type",
                table: "monthly_reports");

            migrationBuilder.DropColumn(
                name: "dynamic_data",
                table: "monthly_reports");

            migrationBuilder.DropColumn(
                name: "group",
                table: "monthly_report_items");

            migrationBuilder.DropColumn(
                name: "business_type",
                table: "customers");

            migrationBuilder.AddCheckConstraint(
                name: "CK_TimeFrame_Year",
                table: "time_frames",
                sql: "year >= EXTRACT(YEAR FROM CURRENT_DATE)");

            migrationBuilder.CreateIndex(
                name: "IX_plan_new_customers_customer_id",
                table: "plan_new_customers",
                column: "customer_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_monthly_report_items_monthly_report_id",
                table: "monthly_report_items",
                column: "monthly_report_id");

            migrationBuilder.AddForeignKey(
                name: "FK_customer_time_frames_rel_customers_customer_id",
                table: "customer_time_frames_rel",
                column: "customer_id",
                principalTable: "customers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_monthly_report_items_monthly_reports_monthly_report_id",
                table: "monthly_report_items",
                column: "monthly_report_id",
                principalTable: "monthly_reports",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
