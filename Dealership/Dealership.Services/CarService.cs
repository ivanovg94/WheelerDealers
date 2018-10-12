using Dealership.Data.Models;
using Dealership.Data.UnitOfWork;
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
        private readonly IUnitOfWork unitOfWork;

        public CarService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Car CreateCar(string brandName, string model, short horsePower, short engineCapacity,
            DateTime productionDate, decimal price, string bodyTypeName, string colorName, string colorType,
            string fuelTypeName, string gearboxTypeName, int numOfGears)
        {
            if (brandName.Length < 2 || brandName.Length > 25)
            {
                throw new ServiceException("The name of brand cannot be less than 2 symbols or more than 25 symbols.");
            }

            var brand = this.unitOfWork.GetRepository<Brand>().All().FirstOrDefault(b => b.Name == brandName);

            if (brand == null)
            {
                brand = new Brand() { Name = brandName };
                this.unitOfWork.GetRepository<Brand>().Add(brand);
                this.unitOfWork.SaveChanges();
            }

            var bodyType = this.unitOfWork.GetRepository<BodyType>().All().FirstOrDefault(c => c.Name == bodyTypeName);
            if (bodyType == null) { throw new ServiceException($"There is no body type with name \"{bodyTypeName}\"."); }

            var color = this.unitOfWork.GetRepository<Color>().All()
                                                              .Include(c => c.ColorType)
                                                              .FirstOrDefault(c => c.Name == colorName
                                                               && c.ColorType.Name == colorType);
            var colorTypeFromDatabase = this.unitOfWork.GetRepository<ColorType>().All()
                                       .FirstOrDefault(ct => ct.Name == colorType);
            if (colorTypeFromDatabase == null) { throw new InvalidOperationException($"There is no color type with name \"{bodyTypeName}\"."); }

            if (color == null)
            {
                color = new Color { Name = colorName, ColorType = colorTypeFromDatabase };
                this.unitOfWork.GetRepository<Color>().Add(color);
                this.unitOfWork.SaveChanges();
            }

            var fuelType = this.unitOfWork.GetRepository<FuelType>().All()
                                          .FirstOrDefault(f => f.Name == fuelTypeName);
            if (fuelType == null)
            {
                throw new ServiceException($"There is no fuel with name \"{fuelTypeName}\".");
            }

            var gearbox = this.unitOfWork.GetRepository<Gearbox>().All()
                .FirstOrDefault(g => g.GearType.Name == gearboxTypeName
                                  && g.NumberOfGears == numOfGears);
            if (gearbox == null)
            {
                throw new ServiceException($"There is no such a gearbox.");
            }

            var newCar = new Car()
            {
                BrandId = brand.Id,
                Brand = brand,
                Model = model,
                HorsePower = horsePower,
                EngineCapacity = engineCapacity,
                ProductionDate = productionDate,
                Price = price,
                BodyType = bodyType,
                BodyTypeId = bodyType.Id,
                Color = color,
                ColorId = color.Id,
                FuelType = fuelType,
                FuelTypeId = fuelType.Id,
                GearBox = gearbox,
                GearBoxId = gearbox.Id
            };

            return newCar;
        }

        public void AddCar(Car car)
        {
            if (car == null)
            {
                throw new ServiceException("Car doesn't exist!");
            }
            this.unitOfWork.GetRepository<Car>().Add(car);
            this.unitOfWork.SaveChanges();
        }

        public void AddCars(ICollection<Car> cars)
        {
            foreach (var car in cars)
            {
                if (car != null)
                {
                    this.unitOfWork.GetRepository<Car>().Add(car);
                }
            }
            this.unitOfWork.SaveChanges();
        }

        public IList<Car> GetCars(bool filterSold, string order)
        {
            var querry = this.unitOfWork.GetRepository<Car>().All()
                                            .Where(c => c.IsSold == filterSold)
                                            .Include(c => c.Brand)
                                            .Include(c => c.CarsExtras)
                                                 .ThenInclude(ce => ce.Extra)
                                            .Include(c => c.BodyType)
                                            .Include(c => c.Color)
                                                .ThenInclude(co => co.ColorType)
                                            .Include(c => c.FuelType)
                                            .Include(c => c.GearBox)
                                                .ThenInclude(gb => gb.GearType);

            if (order.ToLower() == "asc")
            {
                return querry.OrderBy(c => c.Id).ToList();
            }

            else if (order.ToLower() == "desc")
            {
                return querry.OrderByDescending(c => c.Id).ToList();
            }
            else
            {
                return querry.ToList();
            }
        }

        public IList<Car> GetCars(string direction)
        {
            var querry = this.unitOfWork.GetRepository<Car>().All()
                                             .Include(c => c.Brand)
                                             .Include(c => c.CarsExtras)
                                                  .ThenInclude(ce => ce.Extra)
                                             .Include(c => c.BodyType)
                                             .Include(c => c.Color)
                                                 .ThenInclude(co => co.ColorType)
                                             .Include(c => c.FuelType)
                                             .Include(c => c.GearBox)
                                                 .ThenInclude(gb => gb.GearType);

            if (direction.ToLower() == "desc")
            {
                return querry.OrderByDescending(c => c.Id).ToList();
            }
            else
            {
                return querry.OrderBy(c => c.Id).ToList();
            }
        }

        public Car GetCar(int id)
        {
            var car = this.unitOfWork.GetRepository<Car>().All()
                                           .Where(c => c.Id == id)
                                           .Include(c => c.Brand)
                                           .Include(c => c.CarsExtras)
                                                .ThenInclude(ce => ce.Extra)
                                           .Include(c => c.BodyType)
                                           .Include(c => c.Color)
                                               .ThenInclude(co => co.ColorType)
                                           .Include(c => c.FuelType)
                                           .Include(c => c.GearBox)
                                               .ThenInclude(gb => gb.GearType)
                                           .FirstOrDefault();

            if (car == null)
            {
                throw new ServiceException($"There is no car with ID {id}.");
            }
            return car;
        }

        public Car RemoveCar(int id)
        {
            var car = GetCar(id);
            this.unitOfWork.GetRepository<Car>().Delete(car);
            this.unitOfWork.SaveChanges();

            return car;
        }
    }
}
