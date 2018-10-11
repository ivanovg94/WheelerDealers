using Microsoft.EntityFrameworkCore.Migrations;

namespace Dealership.Data.Migrations
{
    public partial class ChangedChassisIdToBodyTypeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Chassis_BodyTypeId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ChasisId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "BodyTypeId",
                table: "Cars",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Chassis_BodyTypeId",
                table: "Cars",
                column: "BodyTypeId",
                principalTable: "Chassis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Chassis_BodyTypeId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "BodyTypeId",
                table: "Cars",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ChasisId",
                table: "Cars",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Chassis_BodyTypeId",
                table: "Cars",
                column: "BodyTypeId",
                principalTable: "Chassis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
