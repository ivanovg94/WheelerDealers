using Microsoft.EntityFrameworkCore.Migrations;

namespace Dealership.Data.Migrations
{
    public partial class ChangedChassisToBodyType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Chassis_ChasisId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_ChasisId",
                table: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "BodyTypeId",
                table: "Cars",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_BodyTypeId",
                table: "Cars",
                column: "BodyTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Chassis_BodyTypeId",
                table: "Cars",
                column: "BodyTypeId",
                principalTable: "Chassis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Chassis_BodyTypeId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_BodyTypeId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "BodyTypeId",
                table: "Cars");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ChasisId",
                table: "Cars",
                column: "ChasisId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Chassis_ChasisId",
                table: "Cars",
                column: "ChasisId",
                principalTable: "Chassis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
