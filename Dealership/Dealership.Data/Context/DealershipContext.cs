using Dealership.Data.Context.Configurations;
using Dealership.Data.Models;
using Dealership.Data.Models.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Dealership.Data.Context
{
    public class DealershipContext : DbContext, IDealershipContext
    {
        public DealershipContext()
        {

        }

        public DealershipContext(DbContextOptions contextOptions) : base(contextOptions)
        {
            
        }
        public DbSet<Brand> Brands { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<CarsExtras> CarsExtras { get; set; }

        public DbSet<BodyType> Chassis { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Extra> Extras { get; set; }

        public DbSet<FuelType> FuelTypes { get; set; }

        public DbSet<Gearbox> Gearboxes { get; set; }

        public DbSet<GearType> GearTypes { get; set; }

        public DbSet<ColorType> ColorTypes { get; set; }

        public DbSet<User> Users { get; set; }
                
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(ConnectionConfiguration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CarsExtrasConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            
            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            var newlyCreatedEntities = this.ChangeTracker.Entries()
                .Where(e => e.Entity is IEditable && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)));

            foreach (var entry in newlyCreatedEntities)
            {
                var entity = (IEditable)entry.Entity;

                if (entry.State == EntityState.Added && entity.CreatedOn == null)
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BodyType>().HasData(new BodyType { Id = 1, Name = "Sedan", NumberOfDoors = 4 });
            modelBuilder.Entity<BodyType>().HasData(new BodyType { Id = 2, Name = "Coupe", NumberOfDoors = 2 });
            modelBuilder.Entity<BodyType>().HasData(new BodyType { Id = 3, Name = "Cabrio", NumberOfDoors = 2 });
            modelBuilder.Entity<BodyType>().HasData(new BodyType { Id = 4, Name = "Touring", NumberOfDoors = 4 });
            modelBuilder.Entity<BodyType>().HasData(new BodyType { Id = 5, Name = "Suv", NumberOfDoors = 5 });
            modelBuilder.Entity<BodyType>().HasData(new BodyType { Id = 6, Name = "Hatchback", NumberOfDoors = 5 });

            modelBuilder.Entity<GearType>().HasData(new GearType { Id = 1, Name = "Automatic" });
            modelBuilder.Entity<GearType>().HasData(new GearType { Id = 2, Name = "Manual" });

            modelBuilder.Entity<Gearbox>().HasData(new Gearbox { Id = 1, GearTypeId = 1, NumberOfGears = 3 });
            modelBuilder.Entity<Gearbox>().HasData(new Gearbox { Id = 2, GearTypeId = 1, NumberOfGears = 4 });
            modelBuilder.Entity<Gearbox>().HasData(new Gearbox { Id = 3, GearTypeId = 1, NumberOfGears = 5 });
            modelBuilder.Entity<Gearbox>().HasData(new Gearbox { Id = 4, GearTypeId = 1, NumberOfGears = 6 });
            modelBuilder.Entity<Gearbox>().HasData(new Gearbox { Id = 5, GearTypeId = 1, NumberOfGears = 7 });
            modelBuilder.Entity<Gearbox>().HasData(new Gearbox { Id = 6, GearTypeId = 1, NumberOfGears = 8 });
            modelBuilder.Entity<Gearbox>().HasData(new Gearbox { Id = 7, GearTypeId = 2, NumberOfGears = 4 });
            modelBuilder.Entity<Gearbox>().HasData(new Gearbox { Id = 8, GearTypeId = 2, NumberOfGears = 5 });
            modelBuilder.Entity<Gearbox>().HasData(new Gearbox { Id = 9, GearTypeId = 2, NumberOfGears = 6 });

            modelBuilder.Entity<FuelType>().HasData(new FuelType { Id = 1, Name = "Diesel" });
            modelBuilder.Entity<FuelType>().HasData(new FuelType { Id = 2, Name = "Gasoline" });
            modelBuilder.Entity<FuelType>().HasData(new FuelType { Id = 3, Name = "LPG" });
            modelBuilder.Entity<FuelType>().HasData(new FuelType { Id = 4, Name = "Hybrid" });
            modelBuilder.Entity<FuelType>().HasData(new FuelType { Id = 5, Name = "Electic" });

            modelBuilder.Entity<ColorType>().HasData(new ColorType { Id = 1, Name = "Acrylic" });
            modelBuilder.Entity<ColorType>().HasData(new ColorType { Id = 2, Name = "Metalic" });
            modelBuilder.Entity<ColorType>().HasData(new ColorType { Id = 3, Name = "Pearlescent" });
            modelBuilder.Entity<ColorType>().HasData(new ColorType { Id = 4, Name = "Matte" });
            modelBuilder.Entity<ColorType>().HasData(new ColorType { Id = 5, Name = "Xirallic" });

            modelBuilder.Entity<User>().HasData(new User { Id = 1, Username = "admin", Password = "admin", Email = "wheelerDealer@gmail.com", UserType = Enum.Parse<UserType>("Admin") });
        }
    }
}
