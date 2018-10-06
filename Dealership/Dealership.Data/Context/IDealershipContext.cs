﻿using Dealership.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Dealership.Data.Context
{
    public interface IDealershipContext
    {
        DbSet<Brand> Brands { get; set; }

        DbSet<Car> Cars { get; set; }

        DbSet<CarsExtras> CarsExtras { get; set; }

        DbSet<Chassis> Chassis { get; set; }

        DbSet<Color> Colors { get; set; }

        DbSet<Extra> Extras { get; set; }

        DbSet<FuelType> FuelTypes { get; set; }

        DbSet<Gearbox> Gearboxes { get; set; }

        int SaveChanges();
    }
}