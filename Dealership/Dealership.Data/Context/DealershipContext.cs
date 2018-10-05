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

            base.OnModelCreating(modelBuilder);
        }
    }
}
