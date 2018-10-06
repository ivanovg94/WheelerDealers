using Microsoft.EntityFrameworkCore.Migrations;

namespace Dealership.Data.Migrations
{
    public partial class ExpandedSeedPlusAddCommandImplementation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ColorTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Acrylic" },
                    { 2, "Metalic" },
                    { 3, "Pearlescent" },
                    { 4, "Matte" },
                    { 5, "Xirallic" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ColorTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ColorTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ColorTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ColorTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ColorTypes",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
