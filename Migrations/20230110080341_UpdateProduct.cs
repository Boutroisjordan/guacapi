using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuacAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Appellations_AppellationId",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "AppellationId",
                table: "Product",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Appellationd",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Appellations_AppellationId",
                table: "Product",
                column: "AppellationId",
                principalTable: "Appellations",
                principalColumn: "AppellationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Appellations_AppellationId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Appellationd",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "AppellationId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Appellations_AppellationId",
                table: "Product",
                column: "AppellationId",
                principalTable: "Appellations",
                principalColumn: "AppellationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
