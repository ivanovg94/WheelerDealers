using Microsoft.EntityFrameworkCore.Migrations;

namespace Dealership.Data.Migrations
{
    public partial class RemovedNumberOfDoorsFromCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfDoors",
                table: "BodyTypes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "NumberOfDoors",
                table: "BodyTypes",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.UpdateData(
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "NumberOfDoors",
                value: (byte)4);

            migrationBuilder.UpdateData(
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "NumberOfDoors",
                value: (byte)2);

            migrationBuilder.UpdateData(
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "NumberOfDoors",
                value: (byte)2);

            migrationBuilder.UpdateData(
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "NumberOfDoors",
                value: (byte)4);

            migrationBuilder.UpdateData(
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "NumberOfDoors",
                value: (byte)5);

            migrationBuilder.UpdateData(
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "NumberOfDoors",
                value: (byte)5);
        }
    }
}
