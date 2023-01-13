using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuacAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddOffers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_AlcoholTypes_AlcoholTypeId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Appellations_AppellationId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Domains_DomainId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Furnishers_FurnisherId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Regions_RegionId",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Regions",
                table: "Regions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Furnishers",
                table: "Furnishers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Domains",
                table: "Domains");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appellations",
                table: "Appellations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AlcoholTypes",
                table: "AlcoholTypes");

            migrationBuilder.RenameTable(
                name: "Regions",
                newName: "Region");

            migrationBuilder.RenameTable(
                name: "Furnishers",
                newName: "Furnisher");

            migrationBuilder.RenameTable(
                name: "Domains",
                newName: "Domain");

            migrationBuilder.RenameTable(
                name: "Appellations",
                newName: "appellation");

            migrationBuilder.RenameTable(
                name: "AlcoholTypes",
                newName: "alcohol_type");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Region",
                table: "Region",
                column: "RegionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Furnisher",
                table: "Furnisher",
                column: "FurnisherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Domain",
                table: "Domain",
                column: "DomainId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_appellation",
                table: "appellation",
                column: "AppellationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_alcohol_type",
                table: "alcohol_type",
                column: "AlcoholTypeId");

            migrationBuilder.CreateTable(
                name: "Offer",
                columns: table => new
                {
                    OfferId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offer", x => x.OfferId);
                });

            migrationBuilder.CreateTable(
                name: "OfferProduct",
                columns: table => new
                {
                    OffersOfferId = table.Column<int>(type: "int", nullable: false),
                    ProductsProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferProduct", x => new { x.OffersOfferId, x.ProductsProductId });
                    table.ForeignKey(
                        name: "FK_OfferProduct_Offer_OffersOfferId",
                        column: x => x.OffersOfferId,
                        principalTable: "Offer",
                        principalColumn: "OfferId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferProduct_Product_ProductsProductId",
                        column: x => x.ProductsProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfferProduct_ProductsProductId",
                table: "OfferProduct",
                column: "ProductsProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Domain_DomainId",
                table: "Product",
                column: "DomainId",
                principalTable: "Domain",
                principalColumn: "DomainId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Furnisher_FurnisherId",
                table: "Product",
                column: "FurnisherId",
                principalTable: "Furnisher",
                principalColumn: "FurnisherId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Region_RegionId",
                table: "Product",
                column: "RegionId",
                principalTable: "Region",
                principalColumn: "RegionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_alcohol_type_AlcoholTypeId",
                table: "Product",
                column: "AlcoholTypeId",
                principalTable: "alcohol_type",
                principalColumn: "AlcoholTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_appellation_AppellationId",
                table: "Product",
                column: "AppellationId",
                principalTable: "appellation",
                principalColumn: "AppellationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Domain_DomainId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Furnisher_FurnisherId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Region_RegionId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_alcohol_type_AlcoholTypeId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_appellation_AppellationId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "OfferProduct");

            migrationBuilder.DropTable(
                name: "Offer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Region",
                table: "Region");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Furnisher",
                table: "Furnisher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Domain",
                table: "Domain");

            migrationBuilder.DropPrimaryKey(
                name: "PK_appellation",
                table: "appellation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_alcohol_type",
                table: "alcohol_type");

            migrationBuilder.RenameTable(
                name: "Region",
                newName: "Regions");

            migrationBuilder.RenameTable(
                name: "Furnisher",
                newName: "Furnishers");

            migrationBuilder.RenameTable(
                name: "Domain",
                newName: "Domains");

            migrationBuilder.RenameTable(
                name: "appellation",
                newName: "Appellations");

            migrationBuilder.RenameTable(
                name: "alcohol_type",
                newName: "AlcoholTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Regions",
                table: "Regions",
                column: "RegionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Furnishers",
                table: "Furnishers",
                column: "FurnisherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Domains",
                table: "Domains",
                column: "DomainId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appellations",
                table: "Appellations",
                column: "AppellationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AlcoholTypes",
                table: "AlcoholTypes",
                column: "AlcoholTypeId");

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
                principalColumn: "AppellationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Domains_DomainId",
                table: "Product",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "DomainId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Furnishers_FurnisherId",
                table: "Product",
                column: "FurnisherId",
                principalTable: "Furnishers",
                principalColumn: "FurnisherId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Regions_RegionId",
                table: "Product",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "RegionID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
