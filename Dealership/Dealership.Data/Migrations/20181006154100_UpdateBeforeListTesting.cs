using Microsoft.EntityFrameworkCore.Migrations;

namespace Dealership.Data.Migrations
{
    public partial class UpdateBeforeListTesting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "NumberOfDoors",
                table: "Chassis",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "Chassis",
                keyColumn: "Id",
                keyValue: 1,
                column: "NumberOfDoors",
                value: (byte)4);

            migrationBuilder.UpdateData(
                table: "Chassis",
                keyColumn: "Id",
                keyValue: 2,
                column: "NumberOfDoors",
                value: (byte)2);

            migrationBuilder.UpdateData(
                table: "Chassis",
                keyColumn: "Id",
                keyValue: 3,
                column: "NumberOfDoors",
                value: (byte)2);

            migrationBuilder.UpdateData(
                table: "Chassis",
                keyColumn: "Id",
                keyValue: 4,
                column: "NumberOfDoors",
                value: (byte)4);

            migrationBuilder.UpdateData(
                table: "Chassis",
                keyColumn: "Id",
                keyValue: 5,
                column: "NumberOfDoors",
                value: (byte)5);

            migrationBuilder.UpdateData(
                table: "Chassis",
                keyColumn: "Id",
                keyValue: 6,
                column: "NumberOfDoors",
                value: (byte)5);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NumberOfDoors",
                table: "Chassis",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.UpdateData(
                table: "Chassis",
                keyColumn: "Id",
                keyValue: 1,
                column: "NumberOfDoors",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Chassis",
                keyColumn: "Id",
                keyValue: 2,
                column: "NumberOfDoors",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Chassis",
                keyColumn: "Id",
                keyValue: 3,
                column: "NumberOfDoors",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Chassis",
                keyColumn: "Id",
                keyValue: 4,
                column: "NumberOfDoors",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Chassis",
                keyColumn: "Id",
                keyValue: 5,
                column: "NumberOfDoors",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Chassis",
                keyColumn: "Id",
                keyValue: 6,
                column: "NumberOfDoors",
                value: 5);
        }
    }
}
