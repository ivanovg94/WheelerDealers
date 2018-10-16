using Dealership.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Dealership.Data.Context.Abstract
{
    public interface IDealershipContext
    {
        DbSet<Brand> Brands { get; set; }

        DbSet<Car> Cars { get; set; }

        DbSet<CarsExtras> CarsExtras { get; set; }

        DbSet<BodyType> Chassis { get; set; }

        DbSet<Color> Colors { get; set; }

        DbSet<Extra> Extras { get; set; }

        DbSet<FuelType> FuelTypes { get; set; }

        DbSet<Gearbox> Gearboxes { get; set; }

        DbSet<ColorType> ColorTypes { get; set; }

        DbSet<GearType> GearTypes { get; set; }

        int SaveChanges();
    }
}
