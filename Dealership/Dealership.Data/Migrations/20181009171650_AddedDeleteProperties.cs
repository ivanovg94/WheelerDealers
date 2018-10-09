using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dealership.Data.Migrations
{
    public partial class AddedDeleteProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "GearTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "FuelTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "ColorTypes",
                newName: "Name");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "GearTypes",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "GearTypes",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "GearTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "GearTypes",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Gearboxes",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Gearboxes",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Gearboxes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Gearboxes",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "FuelTypes",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "FuelTypes",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FuelTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "FuelTypes",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Extras",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Extras",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Extras",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Extras",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "ColorTypes",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "ColorTypes",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ColorTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "ColorTypes",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Colors",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Colors",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Colors",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Colors",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Chassis",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Chassis",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Chassis",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Chassis",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Cars",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Brands",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Brands",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Brands",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Brands",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "GearTypes");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "GearTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "GearTypes");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "GearTypes");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Gearboxes");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Gearboxes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Gearboxes");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Gearboxes");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "FuelTypes");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "FuelTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FuelTypes");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "FuelTypes");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Extras");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Extras");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Extras");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Extras");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "ColorTypes");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "ColorTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ColorTypes");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "ColorTypes");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Chassis");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Chassis");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Chassis");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Chassis");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Brands");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "GearTypes",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "FuelTypes",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ColorTypes",
                newName: "Type");
        }
    }
}
