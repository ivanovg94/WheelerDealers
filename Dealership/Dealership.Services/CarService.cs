using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Dealership.Services.Exceptions;
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

        public Car CreateCar(string brandName, string model, short horsePower, short engineCapacity
           , DateTime productionDate, decimal price, string chassisName, string colorName, string colorType, string fuelTypeName, string gearboxTypeName, int numOfGears)
        {
            var brand = this.Context.Brands.FirstOrDefault(b => b.Name == brandName);
            if (brand == null)
            {
                brand = new Brand() { Name = brandName };
                this.Context.Brands.Add(brand);
                this.Context.SaveChanges();
            }

            var chassis = this.Context.Chassis.FirstOrDefault(c => c.Name == chassisName);
            if (chassis == null)
            {
                throw new ChassisNotFoundException($"There is no chassis with name \"{chassisName}\".");
            }

            var color = this.Context.Colors.FirstOrDefault(c => c.Name == colorName);
            if (color == null)
            {
                color = new Color
                {
                    Name = colorName,
                    ColorType = this.Context.ColorTypes.FirstOrDefault(ct => ct.Type == colorType)
                };

                if (color.ColorType == null)
                {
                    throw new ColorTypeNotFoundException($"There is no color type with name \"{chassisName}\".");
                }
                this.Context.Colors.Add(color);
                this.Context.SaveChanges();
            }

            var fuelType = this.Context.FuelTypes.FirstOrDefault(f => f.Type == fuelTypeName);
            if (fuelType == null)
            {
                throw new FuelNotFoundException($"There is no fuel with name \"{fuelTypeName}\".");
            }

            var gearbox = this.Context.Gearboxes.FirstOrDefault(g => g.GearType.Type == gearboxTypeName && g.NumberOfGears == numOfGears);
            if (gearbox == null)
            {
                throw new GearboxNotFoundException($"There is no such a gearbox.");
            }

            var newCar = new Car()
            {
                Brand = brand,
                Model = model,
                HorsePower = horsePower,
                EngineCapacity = engineCapacity,
                ProductionDate = productionDate,
                Price = price,
                Chasis = chassis,
                ChasisId = chassis.Id,
                Color = color,
                ColorId = color.Id,
                FuelType = fuelType,
                FuelTypeId = fuelType.Id,
                GearBox = gearbox,
                GearBoxId = gearbox.Id
            };

            return newCar;
        }

        public Car AddCar(Car car)
        {
            this.Context.Cars.Add(car);
            this.Context.SaveChanges();

            return car;
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

        public Car RemoveCar(int id)
        {
            var car = GetCar(id);

            this.Context.Cars.Remove(car);
            this.Context.SaveChanges();

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

        public Car CreateCar(Brand brand, string model, short horsePower, short engineCapacity, DateTime productionDate, decimal price, Chassis chassis, Color color, FuelType fuelType, Gearbox gearbox)
        {
            throw new NotImplementedException();
        }
    }
}
