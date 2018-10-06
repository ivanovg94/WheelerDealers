﻿// <auto-generated />
using System;
using Dealership.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dealership.Data.Migrations
{
    [DbContext(typeof(DealershipContext))]
    [Migration("20181006124552_ExpandedSeedPlusAddCommandImplementation")]
    partial class ExpandedSeedPlusAddCommandImplementation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Dealership.Data.Models.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("Dealership.Data.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrandId");

                    b.Property<int>("ChasisId");

                    b.Property<int>("ColorId");

                    b.Property<short>("EngineCapacity");

                    b.Property<int>("FuelTypeId");

                    b.Property<int>("GearBoxId");

                    b.Property<short>("HorsePower");

                    b.Property<bool>("IsSold");

                    b.Property<string>("Model");

                    b.Property<decimal>("Price");

                    b.Property<DateTime>("ProductionDate");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("ChasisId");

                    b.HasIndex("ColorId");

                    b.HasIndex("FuelTypeId");

                    b.HasIndex("GearBoxId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("Dealership.Data.Models.CarsExtras", b =>
                {
                    b.Property<int>("CarId");

                    b.Property<int>("ExtraId");

                    b.HasKey("CarId", "ExtraId");

                    b.HasIndex("ExtraId");

                    b.ToTable("CarsExtras");
                });

            modelBuilder.Entity("Dealership.Data.Models.Chassis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("NumberOfDoors");

                    b.HasKey("Id");

                    b.ToTable("Chassis");

                    b.HasData(
                        new { Id = 1, Name = "Sedan", NumberOfDoors = 4 },
                        new { Id = 2, Name = "Coupe", NumberOfDoors = 2 },
                        new { Id = 3, Name = "Cabrio", NumberOfDoors = 2 },
                        new { Id = 4, Name = "Touring", NumberOfDoors = 4 },
                        new { Id = 5, Name = "Suv", NumberOfDoors = 5 },
                        new { Id = 6, Name = "Hatchback", NumberOfDoors = 5 }
                    );
                });

            modelBuilder.Entity("Dealership.Data.Models.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ColorTypeId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("ColorTypeId");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("Dealership.Data.Models.ColorType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("ColorTypes");

                    b.HasData(
                        new { Id = 1, Type = "Acrylic" },
                        new { Id = 2, Type = "Metalic" },
                        new { Id = 3, Type = "Pearlescent" },
                        new { Id = 4, Type = "Matte" },
                        new { Id = 5, Type = "Xirallic" }
                    );
                });

            modelBuilder.Entity("Dealership.Data.Models.Extra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Extras");
                });

            modelBuilder.Entity("Dealership.Data.Models.FuelType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("FuelTypes");

                    b.HasData(
                        new { Id = 1, Type = "Diesel" },
                        new { Id = 2, Type = "Gasoline" },
                        new { Id = 3, Type = "LPG" },
                        new { Id = 4, Type = "Hybrid" },
                        new { Id = 5, Type = "Electic" }
                    );
                });

            modelBuilder.Entity("Dealership.Data.Models.Gearbox", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GearTypeId");

                    b.Property<byte>("NumberOfGears");

                    b.HasKey("Id");

                    b.HasIndex("GearTypeId");

                    b.ToTable("Gearboxes");

                    b.HasData(
                        new { Id = 1, GearTypeId = 1, NumberOfGears = (byte)3 },
                        new { Id = 2, GearTypeId = 1, NumberOfGears = (byte)4 },
                        new { Id = 3, GearTypeId = 1, NumberOfGears = (byte)5 },
                        new { Id = 4, GearTypeId = 1, NumberOfGears = (byte)6 },
                        new { Id = 5, GearTypeId = 1, NumberOfGears = (byte)7 },
                        new { Id = 6, GearTypeId = 1, NumberOfGears = (byte)8 },
                        new { Id = 7, GearTypeId = 2, NumberOfGears = (byte)4 },
                        new { Id = 8, GearTypeId = 2, NumberOfGears = (byte)5 },
                        new { Id = 9, GearTypeId = 2, NumberOfGears = (byte)6 }
                    );
                });

            modelBuilder.Entity("Dealership.Data.Models.GearType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("GearTypes");

                    b.HasData(
                        new { Id = 1, Type = "Automatic" },
                        new { Id = 2, Type = "Manual" }
                    );
                });

            modelBuilder.Entity("Dealership.Data.Models.Car", b =>
                {
                    b.HasOne("Dealership.Data.Models.Brand", "Brand")
                        .WithMany("Cars")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dealership.Data.Models.Chassis", "Chasis")
                        .WithMany("Cars")
                        .HasForeignKey("ChasisId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dealership.Data.Models.Color", "Color")
                        .WithMany("Cars")
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dealership.Data.Models.FuelType", "FuelType")
                        .WithMany("Cars")
                        .HasForeignKey("FuelTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dealership.Data.Models.Gearbox", "GearBox")
                        .WithMany("Cars")
                        .HasForeignKey("GearBoxId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dealership.Data.Models.CarsExtras", b =>
                {
                    b.HasOne("Dealership.Data.Models.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dealership.Data.Models.Extra", "Extra")
                        .WithMany()
                        .HasForeignKey("ExtraId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dealership.Data.Models.Color", b =>
                {
                    b.HasOne("Dealership.Data.Models.ColorType", "ColorType")
                        .WithMany("Colors")
                        .HasForeignKey("ColorTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dealership.Data.Models.Gearbox", b =>
                {
                    b.HasOne("Dealership.Data.Models.GearType", "GearType")
                        .WithMany("Gearboxes")
                        .HasForeignKey("GearTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
