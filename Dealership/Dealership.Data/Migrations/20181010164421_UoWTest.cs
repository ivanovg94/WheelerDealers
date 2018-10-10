using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dealership.Data.Migrations
{
    public partial class UoWTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "CarsExtras",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "CarsExtras",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CarsExtras",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "CarsExtras",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "CarsExtras");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "CarsExtras");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CarsExtras");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "CarsExtras");
        }
    }
}
