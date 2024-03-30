using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialAccounting.Dara.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class addfirstentities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "category",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    title = table.Column<string>(type: "text", nullable: false, comment: "Название категории"),
                    createddate = table.Column<DateTime>(name: "created_date", type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updateddate = table.Column<DateTime>(name: "updated_date", type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category", x => x.id);
                },
                comment: "Категория");

            migrationBuilder.CreateTable(
                name: "file",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    address = table.Column<string>(type: "text", nullable: false),
                    filename = table.Column<string>(name: "file_name", type: "text", nullable: false),
                    size = table.Column<long>(type: "bigint", nullable: false),
                    contenttype = table.Column<string>(name: "content_type", type: "text", nullable: true),
                    photoid = table.Column<Guid>(name: "photo_id", type: "uuid", nullable: false),
                    createddate = table.Column<DateTime>(name: "created_date", type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updateddate = table.Column<DateTime>(name: "updated_date", type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_file", x => x.id);
                },
                comment: "Файл");

            migrationBuilder.CreateTable(
                name: "user",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    login = table.Column<string>(type: "text", nullable: false, comment: "Логин"),
                    passwordhash = table.Column<string>(name: "password_hash", type: "text", nullable: false, comment: "Хеш пароля"),
                    email = table.Column<string>(type: "text", nullable: false, comment: "Электронная почта"),
                    phone = table.Column<string>(type: "text", nullable: true, comment: "Телефон"),
                    createddate = table.Column<DateTime>(name: "created_date", type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updateddate = table.Column<DateTime>(name: "updated_date", type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                },
                comment: "Пользователь");

            migrationBuilder.CreateTable(
                name: "bank_account",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    userid = table.Column<Guid>(name: "user_id", type: "uuid", nullable: false, comment: "Идентификатор пользователя"),
                    balance = table.Column<decimal>(type: "numeric", nullable: false, comment: "Баланс"),
                    createddate = table.Column<DateTime>(name: "created_date", type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updateddate = table.Column<DateTime>(name: "updated_date", type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bank_account", x => x.id);
                    table.ForeignKey(
                        name: "fk_bank_account_users_user_id",
                        column: x => x.userid,
                        principalSchema: "public",
                        principalTable: "user",
                        principalColumn: "id");
                },
                comment: "Банковский счет");

            migrationBuilder.CreateTable(
                name: "financial_transaction",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    categoryid = table.Column<Guid>(name: "category_id", type: "uuid", nullable: false, comment: "Идентификатор категории"),
                    bankaccountid = table.Column<Guid>(name: "bank_account_id", type: "uuid", nullable: false, comment: "Идентификатор счета"),
                    amount = table.Column<decimal>(type: "numeric", nullable: false, comment: "Сумма операции"),
                    type = table.Column<int>(type: "integer", nullable: false, comment: "Тип операции"),
                    actualdate = table.Column<DateTime>(name: "actual_date", type: "timestamp with time zone", nullable: false, comment: "Фактическая дата совершения операции"),
                    comment = table.Column<string>(type: "text", nullable: true, comment: "Комментарий"),
                    createddate = table.Column<DateTime>(name: "created_date", type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updateddate = table.Column<DateTime>(name: "updated_date", type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_financial_transaction", x => x.id);
                    table.ForeignKey(
                        name: "fk_financial_transaction_bank_account_bank_account_id",
                        column: x => x.bankaccountid,
                        principalSchema: "public",
                        principalTable: "bank_account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_financial_transaction_category_category_id",
                        column: x => x.categoryid,
                        principalSchema: "public",
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                },
                comment: "Финансовая операция");

            migrationBuilder.CreateTable(
                name: "transfer",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    amount = table.Column<decimal>(type: "numeric", nullable: false, comment: "Сумма перевода"),
                    frombankaccountid = table.Column<Guid>(name: "from_bank_account_id", type: "uuid", nullable: false, comment: "Идентификатор счета, с которого был отправлен перевод"),
                    tobankaccountid = table.Column<Guid>(name: "to_bank_account_id", type: "uuid", nullable: false, comment: "Идентификатор счета, на который был отправлен перевод"),
                    createddate = table.Column<DateTime>(name: "created_date", type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updateddate = table.Column<DateTime>(name: "updated_date", type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transfer", x => x.id);
                    table.ForeignKey(
                        name: "fk_transfer_bank_account_from_bank_account_id",
                        column: x => x.frombankaccountid,
                        principalSchema: "public",
                        principalTable: "bank_account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_transfer_bank_account_to_bank_account_id",
                        column: x => x.tobankaccountid,
                        principalSchema: "public",
                        principalTable: "bank_account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                },
                comment: "Перевод");

            migrationBuilder.CreateTable(
                name: "photo",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    financialtransactionid = table.Column<Guid>(name: "financial_transaction_id", type: "uuid", nullable: false, comment: "Идентификатор финансовой операции"),
                    fileid = table.Column<Guid>(name: "file_id", type: "uuid", nullable: false, comment: "Идентификатор файла"),
                    createddate = table.Column<DateTime>(name: "created_date", type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updateddate = table.Column<DateTime>(name: "updated_date", type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_photo", x => x.id);
                    table.ForeignKey(
                        name: "fk_photo_file_file_id",
                        column: x => x.fileid,
                        principalSchema: "public",
                        principalTable: "file",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_photo_financial_transaction_financial_transaction_id",
                        column: x => x.financialtransactionid,
                        principalSchema: "public",
                        principalTable: "financial_transaction",
                        principalColumn: "id");
                },
                comment: "Фотография");

            migrationBuilder.CreateIndex(
                name: "ix_bank_account_user_id",
                schema: "public",
                table: "bank_account",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_financial_transaction_bank_account_id",
                schema: "public",
                table: "financial_transaction",
                column: "bank_account_id");

            migrationBuilder.CreateIndex(
                name: "ix_financial_transaction_category_id",
                schema: "public",
                table: "financial_transaction",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_photo_file_id",
                schema: "public",
                table: "photo",
                column: "file_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_photo_financial_transaction_id",
                schema: "public",
                table: "photo",
                column: "financial_transaction_id");

            migrationBuilder.CreateIndex(
                name: "ix_transfer_from_bank_account_id",
                schema: "public",
                table: "transfer",
                column: "from_bank_account_id");

            migrationBuilder.CreateIndex(
                name: "ix_transfer_to_bank_account_id",
                schema: "public",
                table: "transfer",
                column: "to_bank_account_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "photo",
                schema: "public");

            migrationBuilder.DropTable(
                name: "transfer",
                schema: "public");

            migrationBuilder.DropTable(
                name: "file",
                schema: "public");

            migrationBuilder.DropTable(
                name: "financial_transaction",
                schema: "public");

            migrationBuilder.DropTable(
                name: "bank_account",
                schema: "public");

            migrationBuilder.DropTable(
                name: "category",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user",
                schema: "public");
        }
    }
}
