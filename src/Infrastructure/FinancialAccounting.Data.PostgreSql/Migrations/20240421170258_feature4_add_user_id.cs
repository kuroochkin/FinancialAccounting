using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialAccounting.Dara.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class feature4adduserid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                schema: "public",
                table: "financial_transaction",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "Идентификтор пользователя");

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                schema: "public",
                table: "category",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "Идентификтор пользователя");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_id",
                schema: "public",
                table: "financial_transaction");

            migrationBuilder.DropColumn(
                name: "user_id",
                schema: "public",
                table: "category");
        }
    }
}
