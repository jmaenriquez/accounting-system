using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountingSystem.Migrations
{
    /// <inheritdoc />
    public partial class JournalSoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "remarks",
                table: "Journal",
                newName: "enterRemark");

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Journal",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Journal");

            migrationBuilder.RenameColumn(
                name: "enterRemark",
                table: "Journal",
                newName: "remarks");
        }
    }
}
