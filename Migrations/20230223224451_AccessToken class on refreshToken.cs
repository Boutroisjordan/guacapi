using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuacAPI.Migrations
{
    /// <inheritdoc />
    public partial class AccessTokenclassonrefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "newTokenExpires",
                table: "RefreshToken",
                newName: "NewTokenExpires");

            migrationBuilder.RenameColumn(
                name: "newToken",
                table: "RefreshToken",
                newName: "NewToken");

            migrationBuilder.RenameColumn(
                name: "TokenExpires",
                table: "RefreshToken",
                newName: "AccessTokenExpires");

            migrationBuilder.RenameColumn(
                name: "Token",
                table: "RefreshToken",
                newName: "AccessToken");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NewTokenExpires",
                table: "RefreshToken",
                newName: "newTokenExpires");

            migrationBuilder.RenameColumn(
                name: "NewToken",
                table: "RefreshToken",
                newName: "newToken");

            migrationBuilder.RenameColumn(
                name: "AccessTokenExpires",
                table: "RefreshToken",
                newName: "TokenExpires");

            migrationBuilder.RenameColumn(
                name: "AccessToken",
                table: "RefreshToken",
                newName: "Token");
        }
    }
}
