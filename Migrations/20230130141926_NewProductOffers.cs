using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuacAPI.Migrations
{
    /// <inheritdoc />
    public partial class NewProductOffers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductOffers_Offer_OfferId",
                table: "ProductOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOffers_Product_ProductId",
                table: "ProductOffers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductOffers",
                table: "ProductOffers");

            migrationBuilder.RenameTable(
                name: "ProductOffers",
                newName: "ProductOffer");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOffers_OfferId",
                table: "ProductOffer",
                newName: "IX_ProductOffer_OfferId");

            migrationBuilder.AddColumn<int>(
                name: "ProductOfferId",
                table: "ProductOffer",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductOffer",
                table: "ProductOffer",
                column: "ProductOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOffer_ProductId",
                table: "ProductOffer",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOffer_Offer_OfferId",
                table: "ProductOffer",
                column: "OfferId",
                principalTable: "Offer",
                principalColumn: "OfferId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOffer_Product_ProductId",
                table: "ProductOffer",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductOffer_Offer_OfferId",
                table: "ProductOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOffer_Product_ProductId",
                table: "ProductOffer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductOffer",
                table: "ProductOffer");

            migrationBuilder.DropIndex(
                name: "IX_ProductOffer_ProductId",
                table: "ProductOffer");

            migrationBuilder.DropColumn(
                name: "ProductOfferId",
                table: "ProductOffer");

            migrationBuilder.RenameTable(
                name: "ProductOffer",
                newName: "ProductOffers");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOffer_OfferId",
                table: "ProductOffers",
                newName: "IX_ProductOffers_OfferId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductOffers",
                table: "ProductOffers",
                columns: new[] { "ProductId", "OfferId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOffers_Offer_OfferId",
                table: "ProductOffers",
                column: "OfferId",
                principalTable: "Offer",
                principalColumn: "OfferId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOffers_Product_ProductId",
                table: "ProductOffers",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
