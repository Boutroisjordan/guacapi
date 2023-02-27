using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuacAPI.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "VerifiedAt" },
                values: new object[] { new DateTime(2023, 2, 27, 14, 38, 31, 332, DateTimeKind.Local).AddTicks(7390), "$2a$11$8I/ML9XqJR/0CRbh9CUZH.vofgejV4aWoNA6zqF4T8TCkLeX31yhC", new DateTime(2023, 2, 27, 14, 38, 31, 332, DateTimeKind.Local).AddTicks(7440) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "VerifiedAt" },
                values: new object[] { new DateTime(2023, 2, 27, 10, 1, 0, 136, DateTimeKind.Local).AddTicks(9786), "$2a$11$8zeHC3nziIvZw9Do9VZoPu0RRF3sVkoZ/Et5q.9pCO4HimH28bHRq", new DateTime(2023, 2, 27, 10, 1, 0, 136, DateTimeKind.Local).AddTicks(9841) });
        }
    }
}
