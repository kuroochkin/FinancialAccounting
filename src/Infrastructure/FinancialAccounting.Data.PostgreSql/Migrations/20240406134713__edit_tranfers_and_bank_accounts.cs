using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialAccounting.Dara.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class edittranfersandbankaccounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "comment",
                schema: "public",
                table: "transfer",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "title",
                schema: "public",
                table: "bank_account",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "comment",
                schema: "public",
                table: "transfer");

            migrationBuilder.DropColumn(
                name: "title",
                schema: "public",
                table: "bank_account");
        }
    }
}
