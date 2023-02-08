using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuacAPI.Migrations
{
    /// <inheritdoc />
    public partial class anotherOne133 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Offer_offerId",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "offerId",
                table: "Comment",
                newName: "OfferId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_offerId",
                table: "Comment",
                newName: "IX_Comment_OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Offer_OfferId",
                table: "Comment",
                column: "OfferId",
                principalTable: "Offer",
                principalColumn: "OfferId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Offer_OfferId",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "OfferId",
                table: "Comment",
                newName: "offerId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_OfferId",
                table: "Comment",
                newName: "IX_Comment_offerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Offer_offerId",
                table: "Comment",
                column: "offerId",
                principalTable: "Offer",
                principalColumn: "OfferId");
        }
    }
}
