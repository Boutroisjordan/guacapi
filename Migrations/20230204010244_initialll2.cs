using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuacAPI.Migrations
{
    /// <inheritdoc />
    public partial class initialll2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Domain",
                columns: new[] { "DomainId", "Name" },
                values: new object[] { 1, "Domaine 1" });

            migrationBuilder.InsertData(
                table: "Furnisher",
                columns: new[] { "FurnisherId", "City", "Name", "PostalCode", "Siret", "Street" },
                values: new object[] { 1, "budapest", "fournisseur 1", "27000", "29239393", "155 rue des vins" });

            migrationBuilder.InsertData(
                table: "Region",
                columns: new[] { "RegionID", "Name" },
                values: new object[] { 1, "region 1" });

            migrationBuilder.InsertData(
                table: "alcohol_type",
                columns: new[] { "AlcoholTypeId", "label" },
                values: new object[] { 1, "Rouge" });

            migrationBuilder.InsertData(
                table: "appellation",
                columns: new[] { "AppellationId", "Name" },
                values: new object[] { 1, "IGP" });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "AlcoholDegree", "AlcoholTypeId", "AppellationId", "DomainId", "FurnisherId", "Millesime", "Name", "Price", "Reference", "RegionId", "Stock" },
                values: new object[] { 1, 2m, 1, 1, 1, 1, 2010, "product 1", 12, "jndijfndjn", 1, 155 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Domain",
                keyColumn: "DomainId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Furnisher",
                keyColumn: "FurnisherId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "RegionID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "alcohol_type",
                keyColumn: "AlcoholTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "appellation",
                keyColumn: "AppellationId",
                keyValue: 1);
        }
    }
}
