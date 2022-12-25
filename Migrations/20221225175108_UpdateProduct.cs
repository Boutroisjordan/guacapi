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
            migrationBuilder.AddColumn<int>(
                name: "AlcoholTypeId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AppellationId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Appellationd",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RegionId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AlcoholTypes",
                columns: table => new
                {
                    AlcoholTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codetype = table.Column<string>(name: "code_type", type: "nvarchar(max)", nullable: true),
                    label = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlcoholTypes", x => x.AlcoholTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Appellations",
                columns: table => new
                {
                    AppellationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appellations", x => x.AppellationId);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    RegionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.RegionID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_AlcoholTypeId",
                table: "Product",
                column: "AlcoholTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_AppellationId",
                table: "Product",
                column: "AppellationId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_RegionId",
                table: "Product",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_AlcoholTypes_AlcoholTypeId",
                table: "Product",
                column: "AlcoholTypeId",
                principalTable: "AlcoholTypes",
                principalColumn: "AlcoholTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Appellations_AppellationId",
                table: "Product",
                column: "AppellationId",
                principalTable: "Appellations",
                principalColumn: "AppellationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Regions_RegionId",
                table: "Product",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "RegionID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_AlcoholTypes_AlcoholTypeId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Appellations_AppellationId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Regions_RegionId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "AlcoholTypes");

            migrationBuilder.DropTable(
                name: "Appellations");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropIndex(
                name: "IX_Product_AlcoholTypeId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_AppellationId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_RegionId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "AlcoholTypeId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "AppellationId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Appellationd",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Product");
        }
    }
}
