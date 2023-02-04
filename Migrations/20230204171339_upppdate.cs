using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuacAPI.Migrations
{
    /// <inheritdoc />
    public partial class upppdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "InvoiceFurnisherProduct");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "InvoiceFurnisher",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "InvoiceFurnisher");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "InvoiceFurnisherProduct",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
