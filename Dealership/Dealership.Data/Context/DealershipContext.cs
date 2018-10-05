using Dealership.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Data.Context
{
    public class DealershipContext : DbContext
    {
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                //    .UseLoggerFactory(loggerFactory)
                    .UseSqlServer("Server=.;Database=Dealership;Trusted_Connection=True;");
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
