using System;
using Dealership.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Dealership.Data.Context
{
    public class DealershipContext : DbContext, IDealershipContext
    {
        public DbSet<Brand> Brands { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<CarsExtras> CarsExtras { get; set; }

        public DbSet<Chassis> Chassis { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Extra> Extras { get; set; }

        public DbSet<FuelType> FuelTypes { get; set; }

        public DbSet<Gearbox> Gearboxes { get; set; }

        public DbSet<GearType> GearTypes { get; set; }

        public DbSet<ColorType> ColorTypes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    //.UseLoggerFactory(loggerFactory)
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarsExtras>()
                .HasKey(ce => new { ce.CarId, ce.ExtraId });

            modelBuilder.Entity<Chassis>().HasData(new Chassis { Id = 1, Name = "Sedan", NumberOfDoors = 4 });
            modelBuilder.Entity<Chassis>().HasData(new Chassis { Id = 2, Name = "Coupe", NumberOfDoors = 2 });
            modelBuilder.Entity<Chassis>().HasData(new Chassis { Id = 3, Name = "Cabrio", NumberOfDoors = 2 });
            modelBuilder.Entity<Chassis>().HasData(new Chassis { Id = 4, Name = "Touring", NumberOfDoors = 4 });
            modelBuilder.Entity<Chassis>().HasData(new Chassis { Id = 5, Name = "Suv", NumberOfDoors = 5 });
            modelBuilder.Entity<Chassis>().HasData(new Chassis { Id = 6, Name = "Hatchback", NumberOfDoors = 5 });

            modelBuilder.Entity<GearType>().HasData(new GearType { Id = 1, Type = "Automatic" });
            modelBuilder.Entity<GearType>().HasData(new GearType { Id = 2, Type = "Manual" });

            modelBuilder.Entity<Gearbox>().HasData(new Gearbox { Id = 1, GearTypeId = 1, NumberOfGears = 3 });
            modelBuilder.Entity<Gearbox>().HasData(new Gearbox { Id = 2, GearTypeId = 1, NumberOfGears = 4 });
            modelBuilder.Entity<Gearbox>().HasData(new Gearbox { Id = 3, GearTypeId = 1, NumberOfGears = 5 });
            modelBuilder.Entity<Gearbox>().HasData(new Gearbox { Id = 4, GearTypeId = 1, NumberOfGears = 6 });
            modelBuilder.Entity<Gearbox>().HasData(new Gearbox { Id = 5, GearTypeId = 1, NumberOfGears = 7 });
            modelBuilder.Entity<Gearbox>().HasData(new Gearbox { Id = 6, GearTypeId = 1, NumberOfGears = 8 });
            modelBuilder.Entity<Gearbox>().HasData(new Gearbox { Id = 7, GearTypeId = 2, NumberOfGears = 4 });
            modelBuilder.Entity<Gearbox>().HasData(new Gearbox { Id = 8, GearTypeId = 2, NumberOfGears = 5 });
            modelBuilder.Entity<Gearbox>().HasData(new Gearbox { Id = 9, GearTypeId = 2, NumberOfGears = 6 });

            modelBuilder.Entity<FuelType>().HasData(new FuelType { Id = 1, Type = "Diesel" });
            modelBuilder.Entity<FuelType>().HasData(new FuelType { Id = 2, Type = "Gasoline" });
            modelBuilder.Entity<FuelType>().HasData(new FuelType { Id = 3, Type = "LPG" });
            modelBuilder.Entity<FuelType>().HasData(new FuelType { Id = 4, Type = "Hybrid" });
            modelBuilder.Entity<FuelType>().HasData(new FuelType { Id = 5, Type = "Electic" });

            modelBuilder.Entity<ColorType>().HasData(new ColorType { Id = 1, Type = "Acrylic" });
            modelBuilder.Entity<ColorType>().HasData(new ColorType { Id = 2, Type = "Metalic" });
            modelBuilder.Entity<ColorType>().HasData(new ColorType { Id = 3, Type = "Pearlescent" });
            modelBuilder.Entity<ColorType>().HasData(new ColorType { Id = 4, Type = "Matte" });
            modelBuilder.Entity<ColorType>().HasData(new ColorType { Id = 5, Type = "Xirallic" });

            base.OnModelCreating(modelBuilder);

        }


    }
}
