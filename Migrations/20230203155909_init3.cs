using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuacAPI.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceFurnisherProduct_InvoiceFurnisher_InvoiceFurnisherId",
                table: "InvoiceFurnisherProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceFurnisherProduct_Product_ProductId",
                table: "InvoiceFurnisherProduct");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceFurnisherProduct_InvoiceFurnisher_InvoiceFurnisherId",
                table: "InvoiceFurnisherProduct",
                column: "InvoiceFurnisherId",
                principalTable: "InvoiceFurnisher",
                principalColumn: "InvoiceFurnisherId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceFurnisherProduct_Product_ProductId",
                table: "InvoiceFurnisherProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceFurnisherProduct_InvoiceFurnisher_InvoiceFurnisherId",
                table: "InvoiceFurnisherProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceFurnisherProduct_Product_ProductId",
                table: "InvoiceFurnisherProduct");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceFurnisherProduct_InvoiceFurnisher_InvoiceFurnisherId",
                table: "InvoiceFurnisherProduct",
                column: "InvoiceFurnisherId",
                principalTable: "InvoiceFurnisher",
                principalColumn: "InvoiceFurnisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceFurnisherProduct_Product_ProductId",
                table: "InvoiceFurnisherProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId");
        }
    }
}
