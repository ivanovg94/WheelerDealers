using Microsoft.EntityFrameworkCore.Migrations;

namespace Dealership.Data.Migrations
{
    public partial class ChangedBodyTypeTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Chassis_BodyTypeId",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chassis",
                table: "Chassis");

            migrationBuilder.RenameTable(
                name: "Chassis",
                newName: "BodyTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BodyTypes",
                table: "BodyTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_BodyTypes_BodyTypeId",
                table: "Cars",
                column: "BodyTypeId",
                principalTable: "BodyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_BodyTypes_BodyTypeId",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BodyTypes",
                table: "BodyTypes");

            migrationBuilder.RenameTable(
                name: "BodyTypes",
                newName: "Chassis");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chassis",
                table: "Chassis",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Chassis_BodyTypeId",
                table: "Cars",
                column: "BodyTypeId",
                principalTable: "Chassis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
