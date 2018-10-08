using Dealership.Client.Exceptions;
using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dealership.Services
{
    public class CarService : ICarService
    {
        private IDealershipContext Context;

        public CarService(IDealershipContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Car CreateCar(Brand brand, string model, short horsePower, short engineCapacity
           , DateTime productionDate, decimal price, Chassis chassis, Color color, FuelType fuelType, Gearbox gearbox)
        {
            var newCar = new Car()
            {
                Brand = brand,
                Model = model,
                HorsePower = horsePower,
                EngineCapacity = engineCapacity,
                ProductionDate = productionDate,
                Price = price,
                Chasis = chassis,
                Color = color,
                FuelType = fuelType,
                GearBox = gearbox
            };

            return newCar;
        }

        public Car AddCar(Brand brand, string model, short horsePower, short engineCapacity
            , DateTime productionDate, decimal price, Chassis chassis, Color color, FuelType fuelType, Gearbox gearbox)
        {
            //logic
            var newCar = CreateCar(brand, model, horsePower, engineCapacity, productionDate, price, chassis, color, fuelType, gearbox);

            Context.Cars.Add(newCar);
            Context.SaveChanges();

            return newCar;
        }

        public IEnumerable<Car> GetCars(bool filterSold, string direction)
        {
            var querry = this.Context.Cars.Where(c => c.IsSold == filterSold)
                                           .Include(c => c.Brand)
                                           .Include(c => c.CarsExtras)
                                                .ThenInclude(ce => ce.Extra)
                                           .Include(c => c.Chasis)
                                           .Include(c => c.Color)
                                               .ThenInclude(co => co.ColorType)
                                           .Include(c => c.FuelType)
                                           .Include(c => c.GearBox)
                                               .ThenInclude(gb => gb.GearType);

            if (direction.ToLower() == "asc")
            {
                return querry.OrderBy(c => c.Id).ToList();
            }
            else if (direction.ToLower() == "desc")
            {
                return querry.OrderByDescending(c => c.Id).ToList();
            }
            else { throw new InvalidOperationException("Invalid direction!"); }
        }

        public Car GetCar(int id)
        {
            var car = this.Context.Cars.Where(c => c.Id == id)
                                           .Include(c => c.Brand)
                                           .Include(c => c.CarsExtras)
                                                .ThenInclude(ce => ce.Extra)
                                           .Include(c => c.Chasis)
                                           .Include(c => c.Color)
                                               .ThenInclude(co => co.ColorType)
                                           .Include(c => c.FuelType)
                                           .Include(c => c.GearBox)
                                               .ThenInclude(gb => gb.GearType)
                                           .FirstOrDefault();

            if (car == null)
            {
                throw new CarNotFoundException($"There is no car with ID {id}.");
            }

            return car;
        }

        public Brand GetBrand(string brandName)
        {
            var brand = this.Context.Brands.FirstOrDefault(b => b.Name == brandName);
            if (brand == null)
            {
                throw new BrandNotFoundException($"There is no brand with name \"{brandName}\".");
            }

            return brand;
        }
    }
}
