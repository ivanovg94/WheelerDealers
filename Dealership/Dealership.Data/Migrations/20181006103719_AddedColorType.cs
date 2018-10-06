using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dealership.Data.Migrations
{
    public partial class AddedColorType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Colors");

            migrationBuilder.AddColumn<int>(
                name: "ColorTypeId",
                table: "Colors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ColorTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Colors_ColorTypeId",
                table: "Colors",
                column: "ColorTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Colors_ColorTypes_ColorTypeId",
                table: "Colors",
                column: "ColorTypeId",
                principalTable: "ColorTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colors_ColorTypes_ColorTypeId",
                table: "Colors");

            migrationBuilder.DropTable(
                name: "ColorTypes");

            migrationBuilder.DropIndex(
                name: "IX_Colors_ColorTypeId",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "ColorTypeId",
                table: "Colors");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Colors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Colors",
                nullable: true);
        }
    }
}
