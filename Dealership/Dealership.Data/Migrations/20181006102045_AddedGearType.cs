using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dealership.Data.Migrations
{
    public partial class AddedGearType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Gearboxes");

            migrationBuilder.AddColumn<int>(
                name: "GearTypeId",
                table: "Gearboxes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GearTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gearboxes_GearTypeId",
                table: "Gearboxes",
                column: "GearTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gearboxes_GearTypes_GearTypeId",
                table: "Gearboxes",
                column: "GearTypeId",
                principalTable: "GearTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gearboxes_GearTypes_GearTypeId",
                table: "Gearboxes");

            migrationBuilder.DropTable(
                name: "GearTypes");

            migrationBuilder.DropIndex(
                name: "IX_Gearboxes_GearTypeId",
                table: "Gearboxes");

            migrationBuilder.DropColumn(
                name: "GearTypeId",
                table: "Gearboxes");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Gearboxes",
                nullable: true);
        }
    }
}
