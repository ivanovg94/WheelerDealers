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
                throw new ArgumentOutOfRangeException("The name of brand cannot be less than 2 symbols or more than 25 symbols.");
            }

            var brand = this.unitOfWork.GetRepository<Brand>().All().FirstOrDefault(b => b.Name == brandName);

            if (brand == null)
            {
                brand = new Brand() { Name = brandName };
                this.unitOfWork.GetRepository<Brand>().Add(brand);
                this.unitOfWork.SaveChanges();
            }

            var bodyType = this.unitOfWork.GetRepository<BodyType>().All().FirstOrDefault(c => c.Name == bodyTypeName);
            if (bodyType == null) { throw new InvalidOperationException($"There is no body type with name \"{bodyTypeName}\"."); }

            var color = this.unitOfWork.GetRepository<Color>().All().FirstOrDefault(c => c.Name == colorName);
            if (color == null)
            {
                color = new Color
                {
                    Name = colorName,
                    ColorType = this.unitOfWork.GetRepository<ColorType>().All()
                                               .FirstOrDefault(ct => ct.Name == colorType)
                };

                if (color.ColorType == null) { throw new InvalidOperationException($"There is no color type with name \"{bodyTypeName}\"."); }
                this.unitOfWork.GetRepository<Color>().Add(color);
                this.unitOfWork.SaveChanges();
            }

            var fuelType = this.unitOfWork.GetRepository<FuelType>().All()
                                          .FirstOrDefault(f => f.Name == fuelTypeName);
            if (fuelType == null)
            {
                throw new InvalidOperationException($"There is no fuel with name \"{fuelTypeName}\".");
            }

            var gearbox = this.unitOfWork.GetRepository<Gearbox>().All()
                .FirstOrDefault(g => g.GearType.Name == gearboxTypeName
                                  && g.NumberOfGears == numOfGears);
            if (gearbox == null)
            {
                throw new InvalidOperationException($"There is no such a gearbox.");
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
                throw new ArgumentNullException("Car doesn't exist!");
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
                throw new CarNotFoundException($"There is no car with ID {id}.");
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
        ////
        //public void EditBrand(string[] parameters) // works but must include navigation props tables !
        //{
        //    int id = int.Parse(parameters[0]);
        //    string newValue = parameters[1];
        //    string secondNewValue = "";
        //    if (parameters.Length == 3)
        //    {
        //        secondNewValue = parameters[2];

        //    }

        //    var car = GetCar(id);

        //    Brand newBrand = unitOfWork.GetRepository<Brand>().All().FirstOrDefault(b => b.Name == newValue);

        //    if (newBrand == null) { newBrand = new Brand() { Name = newValue }; }
        //    car.Brand = newBrand;
        //    unitOfWork.SaveChanges();

        //}

        //public void EditModel(string[] parameters)
        //{
        //    var id = int.Parse(parameters[0]);
        //    var newValue = parameters[1];

        //    var car = GetCar(id);
        //    car.Model = newValue;
        //    unitOfWork.SaveChanges();
        //}

        //public void EditHorsePower(string[] parameters)
        //{
        //    var id = int.Parse(parameters[0]);
        //    var newValue = parameters[1];

        //    var car = GetCar(id);
        //    car.HorsePower = short.Parse(newValue);
        //    unitOfWork.SaveChanges();
        //}

        //public void EditEngineCapacity(string[] parameters)
        //{
        //    var id = int.Parse(parameters[0]);
        //    var newValue = parameters[1];

        //    var car = GetCar(id);
        //    car.EngineCapacity = short.Parse(newValue);
        //    unitOfWork.SaveChanges();
        //}

        //public void EditIsSold(string[] parameters)
        //{
        //    var id = int.Parse(parameters[0]);
        //    var newValue = parameters[1];

        //    var car = GetCar(id);
        //    car.IsSold = bool.Parse(newValue);
        //    unitOfWork.SaveChanges();
        //}

        //public void EditPrice(string[] parameters)
        //{
        //    var id = int.Parse(parameters[0]);
        //    var newValue = parameters[1];

        //    var car = GetCar(id);
        //    car.Price = decimal.Parse(newValue);
        //    unitOfWork.SaveChanges();
        //}

        //public void EditProductionDate(string[] parameters)
        //{
        //    var id = int.Parse(parameters[0]);
        //    var newValue = parameters[1];

        //    var car = GetCar(id);
        //    car.ProductionDate = DateTime.Parse(newValue);
        //    unitOfWork.SaveChanges();
        //}

        //public void EditBodyType(string[] parameters)
        //{
        //    var id = int.Parse(parameters[0]);
        //    var newValue = parameters[1];

        //    var newBodyType = this.unitOfWork.GetRepository<BodyType>().All().FirstOrDefault(ch => ch.Name == newValue);

        //    if (newBodyType == null)
        //    {
        //        throw new ArgumentException("Invalid body type!");
        //    }

        //    var car = GetCar(id);
        //    car.BodyType = newBodyType;
        //    unitOfWork.SaveChanges();
        //}

        //public void EditColor(string[] parameters)
        //{
        //    var id = int.Parse(parameters[0]);
        //    var newColorValue = parameters[1];
        //    var newColorType = parameters[2];
        //    var car = GetCar(id);

        //    var newColor = unitOfWork.GetRepository<Color>().All().FirstOrDefault(c => c.Name == newColorValue);

        //    if (newColor == null)
        //    {
        //        var newType = unitOfWork.GetRepository<ColorType>().All().FirstOrDefault(gt => gt.Name == newColorType);

        //        if (newType == null)
        //        {
        //            throw new ArgumentException("Invalid color type!");
        //        }
        //        newColor = new Color()
        //        {
        //            Name = newColorValue,
        //            ColorType = newType

        //        };
        //        unitOfWork.GetRepository<Color>().Add(newColor);

        //    }

        //    car.Color = newColor;
        //    unitOfWork.SaveChanges();
        //}

        //public void EditColorType(string[] parameters)
        //{
        //    var id = int.Parse(parameters[0]);
        //    var newValue = parameters[1];

        //    var car = GetCar(id);

        //    ColorType newColorType = unitOfWork.GetRepository<ColorType>().All().First(ct => ct.Name == newValue);

        //    if (newColorType == null)
        //    {
        //        throw new ArgumentNullException("Color type not exist!");
        //    }

        //    car.Color.ColorType = newColorType;
        //    unitOfWork.SaveChanges();
        //}

        //public void EditFuelType(string[] parameters)
        //{
        //    var id = int.Parse(parameters[0]);
        //    var newValue = parameters[1];

        //    var car = GetCar(id);

        //    var newFuelType = unitOfWork.GetRepository<FuelType>().All().FirstOrDefault(ft => ft.Name == newValue);

        //    if (newFuelType != null)
        //    {
        //        car.FuelType = newFuelType;
        //        unitOfWork.SaveChanges();
        //    }
        //    else { throw new ArgumentException($"Fuel type :{newValue} not exist!"); }

        //}

        //public void EditGearbox(string[] parameters) // works but must include navigation props tables !
        //{
        //    var id = int.Parse(parameters[0]);
        //    var newValue = parameters[1];

        //    var car = GetCar(id);

        //    GearType newGearType = unitOfWork.GetRepository<GearType>().All().First(gt => gt.Name == newValue);

        //    if (newGearType == null)
        //    {
        //        throw new ArgumentException($"Gearbox:{newValue} not exist!");
        //    }
        //    car.GearBox.GearType = newGearType;
        //    unitOfWork.SaveChanges();

        //}
    }
}
