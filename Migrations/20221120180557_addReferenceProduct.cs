using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuacAPI.Migrations
{
    /// <inheritdoc />
    public partial class addReferenceProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Furnishers",
                newName: "FurnisherId");

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "FurnisherId",
                table: "Furnishers",
                newName: "Id");
        }
    }
}
