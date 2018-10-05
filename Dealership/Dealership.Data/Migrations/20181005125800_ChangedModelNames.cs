using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dealership.Data.Migrations
{
    public partial class ChangedModelNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Brand_BrandId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Car_Chasis_ChasisId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Car_Color_ColorId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Car_FuelType_FuelTypeId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Car_GearBox_GearBoxId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_CarsExtras_Car_CarId",
                table: "CarsExtras");

            migrationBuilder.DropForeignKey(
                name: "FK_CarsExtras_Extra_ExtraId",
                table: "CarsExtras");

            migrationBuilder.DropTable(
                name: "Chasis");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GearBox",
                table: "GearBox");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FuelType",
                table: "FuelType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Extra",
                table: "Extra");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Color",
                table: "Color");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Car",
                table: "Car");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brand",
                table: "Brand");

            migrationBuilder.RenameTable(
                name: "GearBox",
                newName: "Gearboxes");

            migrationBuilder.RenameTable(
                name: "FuelType",
                newName: "FuelTypes");

            migrationBuilder.RenameTable(
                name: "Extra",
                newName: "Extras");

            migrationBuilder.RenameTable(
                name: "Color",
                newName: "Colors");

            migrationBuilder.RenameTable(
                name: "Car",
                newName: "Cars");

            migrationBuilder.RenameTable(
                name: "Brand",
                newName: "Brands");

            migrationBuilder.RenameIndex(
                name: "IX_Car_GearBoxId",
                table: "Cars",
                newName: "IX_Cars_GearBoxId");

            migrationBuilder.RenameIndex(
                name: "IX_Car_FuelTypeId",
                table: "Cars",
                newName: "IX_Cars_FuelTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Car_ColorId",
                table: "Cars",
                newName: "IX_Cars_ColorId");

            migrationBuilder.RenameIndex(
                name: "IX_Car_ChasisId",
                table: "Cars",
                newName: "IX_Cars_ChasisId");

            migrationBuilder.RenameIndex(
                name: "IX_Car_BrandId",
                table: "Cars",
                newName: "IX_Cars_BrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gearboxes",
                table: "Gearboxes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FuelTypes",
                table: "FuelTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Extras",
                table: "Extras",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Colors",
                table: "Colors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cars",
                table: "Cars",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brands",
                table: "Brands",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Chassis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<int>(nullable: false),
                    NumberOfDoors = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chassis", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Chassis_ChasisId",
                table: "Cars",
                column: "ChasisId",
                principalTable: "Chassis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Colors_ColorId",
                table: "Cars",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_FuelTypes_FuelTypeId",
                table: "Cars",
                column: "FuelTypeId",
                principalTable: "FuelTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Gearboxes_GearBoxId",
                table: "Cars",
                column: "GearBoxId",
                principalTable: "Gearboxes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarsExtras_Cars_CarId",
                table: "CarsExtras",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarsExtras_Extras_ExtraId",
                table: "CarsExtras",
                column: "ExtraId",
                principalTable: "Extras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Chassis_ChasisId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Colors_ColorId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_FuelTypes_FuelTypeId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Gearboxes_GearBoxId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_CarsExtras_Cars_CarId",
                table: "CarsExtras");

            migrationBuilder.DropForeignKey(
                name: "FK_CarsExtras_Extras_ExtraId",
                table: "CarsExtras");

            migrationBuilder.DropTable(
                name: "Chassis");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gearboxes",
                table: "Gearboxes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FuelTypes",
                table: "FuelTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Extras",
                table: "Extras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Colors",
                table: "Colors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cars",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brands",
                table: "Brands");

            migrationBuilder.RenameTable(
                name: "Gearboxes",
                newName: "GearBox");

            migrationBuilder.RenameTable(
                name: "FuelTypes",
                newName: "FuelType");

            migrationBuilder.RenameTable(
                name: "Extras",
                newName: "Extra");

            migrationBuilder.RenameTable(
                name: "Colors",
                newName: "Color");

            migrationBuilder.RenameTable(
                name: "Cars",
                newName: "Car");

            migrationBuilder.RenameTable(
                name: "Brands",
                newName: "Brand");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_GearBoxId",
                table: "Car",
                newName: "IX_Car_GearBoxId");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_FuelTypeId",
                table: "Car",
                newName: "IX_Car_FuelTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_ColorId",
                table: "Car",
                newName: "IX_Car_ColorId");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_ChasisId",
                table: "Car",
                newName: "IX_Car_ChasisId");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_BrandId",
                table: "Car",
                newName: "IX_Car_BrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GearBox",
                table: "GearBox",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FuelType",
                table: "FuelType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Extra",
                table: "Extra",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Color",
                table: "Color",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Car",
                table: "Car",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brand",
                table: "Brand",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Chasis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<int>(nullable: false),
                    NumberOfDoors = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chasis", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Brand_BrandId",
                table: "Car",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Chasis_ChasisId",
                table: "Car",
                column: "ChasisId",
                principalTable: "Chasis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Color_ColorId",
                table: "Car",
                column: "ColorId",
                principalTable: "Color",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_FuelType_FuelTypeId",
                table: "Car",
                column: "FuelTypeId",
                principalTable: "FuelType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_GearBox_GearBoxId",
                table: "Car",
                column: "GearBoxId",
                principalTable: "GearBox",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarsExtras_Car_CarId",
                table: "CarsExtras",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarsExtras_Extra_ExtraId",
                table: "CarsExtras",
                column: "ExtraId",
                principalTable: "Extra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
