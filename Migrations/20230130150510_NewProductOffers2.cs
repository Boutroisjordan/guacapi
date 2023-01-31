using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuacAPI.Migrations
{
    /// <inheritdoc />
    public partial class NewProductOffers2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductOffer",
                table: "ProductOffer");

            migrationBuilder.DropIndex(
                name: "IX_ProductOffer_ProductId",
                table: "ProductOffer");

            migrationBuilder.DropColumn(
                name: "ProductOfferId",
                table: "ProductOffer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductOffer",
                table: "ProductOffer",
                columns: new[] { "ProductId", "OfferId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductOffer",
                table: "ProductOffer");

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
        }
    }
}
