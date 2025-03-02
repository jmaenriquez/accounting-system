using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AccountingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    accountId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.accountId);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    groupId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    datetime = table.Column<DateOnly>(type: "date", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.groupId);
                });

            migrationBuilder.CreateTable(
                name: "Credit",
                columns: table => new
                {
                    creditId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    accountId = table.Column<int>(type: "integer", nullable: true),
                    amount = table.Column<int>(type: "integer", nullable: false),
                    datetime = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credit", x => x.creditId);
                    table.ForeignKey(
                        name: "FK_Credit_Accounts_accountId",
                        column: x => x.accountId,
                        principalTable: "Accounts",
                        principalColumn: "accountId");
                });

            migrationBuilder.CreateTable(
                name: "Debit",
                columns: table => new
                {
                    debitId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    accountId = table.Column<int>(type: "integer", nullable: true),
                    amount = table.Column<int>(type: "integer", nullable: false),
                    datetime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Debit", x => x.debitId);
                    table.ForeignKey(
                        name: "FK_Debit_Accounts_accountId",
                        column: x => x.accountId,
                        principalTable: "Accounts",
                        principalColumn: "accountId");
                });

            migrationBuilder.CreateTable(
                name: "Amounts",
                columns: table => new
                {
                    amountId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    debitId = table.Column<int>(type: "integer", nullable: true),
                    creditId = table.Column<int>(type: "integer", nullable: true),
                    datetime = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amounts", x => x.amountId);
                    table.ForeignKey(
                        name: "FK_Amounts_Credit_creditId",
                        column: x => x.creditId,
                        principalTable: "Credit",
                        principalColumn: "creditId");
                    table.ForeignKey(
                        name: "FK_Amounts_Debit_debitId",
                        column: x => x.debitId,
                        principalTable: "Debit",
                        principalColumn: "debitId");
                });

            migrationBuilder.CreateTable(
                name: "Journal",
                columns: table => new
                {
                    journal_code = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    datetime = table.Column<DateOnly>(type: "date", nullable: false),
                    accountId = table.Column<int>(type: "integer", nullable: true),
                    debitId = table.Column<int>(type: "integer", nullable: true),
                    creditId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journal", x => x.journal_code);
                    table.ForeignKey(
                        name: "FK_Journal_Accounts_accountId",
                        column: x => x.accountId,
                        principalTable: "Accounts",
                        principalColumn: "accountId");
                    table.ForeignKey(
                        name: "FK_Journal_Credit_creditId",
                        column: x => x.creditId,
                        principalTable: "Credit",
                        principalColumn: "creditId");
                    table.ForeignKey(
                        name: "FK_Journal_Debit_debitId",
                        column: x => x.debitId,
                        principalTable: "Debit",
                        principalColumn: "debitId");
                });

            migrationBuilder.CreateTable(
                name: "Balance",
                columns: table => new
                {
                    balance_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    datetime = table.Column<DateOnly>(type: "date", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    groupId = table.Column<int>(type: "integer", nullable: true),
                    accountId = table.Column<int>(type: "integer", nullable: true),
                    amountId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balance", x => x.balance_Id);
                    table.ForeignKey(
                        name: "FK_Balance_Accounts_accountId",
                        column: x => x.accountId,
                        principalTable: "Accounts",
                        principalColumn: "accountId");
                    table.ForeignKey(
                        name: "FK_Balance_Amounts_amountId",
                        column: x => x.amountId,
                        principalTable: "Amounts",
                        principalColumn: "amountId");
                    table.ForeignKey(
                        name: "FK_Balance_Groups_groupId",
                        column: x => x.groupId,
                        principalTable: "Groups",
                        principalColumn: "groupId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amounts_creditId",
                table: "Amounts",
                column: "creditId");

            migrationBuilder.CreateIndex(
                name: "IX_Amounts_debitId",
                table: "Amounts",
                column: "debitId");

            migrationBuilder.CreateIndex(
                name: "IX_Balance_accountId",
                table: "Balance",
                column: "accountId");

            migrationBuilder.CreateIndex(
                name: "IX_Balance_amountId",
                table: "Balance",
                column: "amountId");

            migrationBuilder.CreateIndex(
                name: "IX_Balance_groupId",
                table: "Balance",
                column: "groupId");

            migrationBuilder.CreateIndex(
                name: "IX_Credit_accountId",
                table: "Credit",
                column: "accountId");

            migrationBuilder.CreateIndex(
                name: "IX_Debit_accountId",
                table: "Debit",
                column: "accountId");

            migrationBuilder.CreateIndex(
                name: "IX_Journal_accountId",
                table: "Journal",
                column: "accountId");

            migrationBuilder.CreateIndex(
                name: "IX_Journal_creditId",
                table: "Journal",
                column: "creditId");

            migrationBuilder.CreateIndex(
                name: "IX_Journal_debitId",
                table: "Journal",
                column: "debitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Balance");

            migrationBuilder.DropTable(
                name: "Journal");

            migrationBuilder.DropTable(
                name: "Amounts");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Credit");

            migrationBuilder.DropTable(
                name: "Debit");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
