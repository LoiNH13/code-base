using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sms.Persistence.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "res_mo",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                service_phone = table.Column<string>(type: "text", nullable: false),
                price_per_mo = table.Column<double>(type: "double precision", nullable: false),
                free_mt_per_mo = table.Column<double>(type: "double precision", nullable: false),
                created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                deleted_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_res_mo", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "res_syntaxes",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                syntax_name = table.Column<string>(type: "text", nullable: false),
                description = table.Column<string>(type: "text", nullable: true),
                syntax_value = table.Column<string>(type: "text", nullable: false),
                syntax_regex = table.Column<string>(type: "text", nullable: true),
                metadata = table.Column<string>(type: "text", nullable: true),
                inactive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                deleted_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_res_syntaxes", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "mo_messages",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                mo_id = table.Column<int>(type: "integer", nullable: false),
                telco = table.Column<string>(type: "text", nullable: false),
                service_num = table.Column<string>(type: "text", nullable: false),
                phone = table.Column<string>(type: "text", nullable: false),
                content = table.Column<string>(type: "text", nullable: false),
                encrypted_message = table.Column<string>(type: "text", nullable: false),
                signature = table.Column<string>(type: "text", nullable: false),
                mo_source = table.Column<string>(type: "text", nullable: false),
                metadata = table.Column<string>(type: "text", nullable: true),
                partner_response = table.Column<string>(type: "text", nullable: true),
                res_mo_id = table.Column<Guid>(type: "uuid", nullable: true),
                created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                deleted_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_mo_messages", x => x.id);
                table.ForeignKey(
                    name: "FK_mo_messages_res_mo_res_mo_id",
                    column: x => x.res_mo_id,
                    principalTable: "res_mo",
                    principalColumn: "id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_mo_messages_res_mo_id",
            table: "mo_messages",
            column: "res_mo_id");

        migrationBuilder.CreateIndex(
            name: "IX_res_mo_service_phone",
            table: "res_mo",
            column: "service_phone",
            unique: true,
            filter: "deleted = false");

        migrationBuilder.CreateIndex(
            name: "IX_res_syntaxes_syntax_value",
            table: "res_syntaxes",
            column: "syntax_value",
            unique: true,
            filter: "deleted = false");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "mo_messages");

        migrationBuilder.DropTable(
            name: "res_syntaxes");

        migrationBuilder.DropTable(
            name: "res_mo");
    }
}
