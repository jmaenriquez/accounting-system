using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AccountingSystem.Migrations
{
    /// <inheritdoc />
    public partial class Journal_Entry_Fk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journal_Accounts_id",
                table: "Journal");

            migrationBuilder.DropForeignKey(
                name: "FK_Journal_Groups_id",
                table: "Journal");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "Journal",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "AccGrpId",
                table: "Journal",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccNameId",
                table: "Journal",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Journal_AccGrpId",
                table: "Journal",
                column: "AccGrpId");

            migrationBuilder.CreateIndex(
                name: "IX_Journal_AccNameId",
                table: "Journal",
                column: "AccNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Journal_Accounts_AccNameId",
                table: "Journal",
                column: "AccNameId",
                principalTable: "Accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Journal_Groups_AccGrpId",
                table: "Journal",
                column: "AccGrpId",
                principalTable: "Groups",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journal_Accounts_AccNameId",
                table: "Journal");

            migrationBuilder.DropForeignKey(
                name: "FK_Journal_Groups_AccGrpId",
                table: "Journal");

            migrationBuilder.DropIndex(
                name: "IX_Journal_AccGrpId",
                table: "Journal");

            migrationBuilder.DropIndex(
                name: "IX_Journal_AccNameId",
                table: "Journal");

            migrationBuilder.DropColumn(
                name: "AccGrpId",
                table: "Journal");

            migrationBuilder.DropColumn(
                name: "AccNameId",
                table: "Journal");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "Journal",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_Journal_Accounts_id",
                table: "Journal",
                column: "id",
                principalTable: "Accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Journal_Groups_id",
                table: "Journal",
                column: "id",
                principalTable: "Groups",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
