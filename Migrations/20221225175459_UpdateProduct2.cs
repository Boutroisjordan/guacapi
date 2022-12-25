using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuacAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProduct2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MillesimeId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Product_MillesimeId",
                table: "Product",
                column: "MillesimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Millesimes_MillesimeId",
                table: "Product",
                column: "MillesimeId",
                principalTable: "Millesimes",
                principalColumn: "MillesimeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Millesimes_MillesimeId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_MillesimeId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "MillesimeId",
                table: "Product");
        }
    }
}
