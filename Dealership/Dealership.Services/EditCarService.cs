using Dealership.Data.Models;
using Dealership.Data.UnitOfWork;
using Dealership.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dealership.Services
{
    public class EditCarService : IEditCarService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICarService carService;

        public EditCarService(IUnitOfWork unitOfWork, ICarService carService)
        {
            this.unitOfWork = unitOfWork;
            this.carService = carService;
        }

        public void EditBrand(string[] parameters) // works but must include navigation props tables !
        {
            int id = int.Parse(parameters[0]);
            string newValue = parameters[1];
            string secondNewValue = "";
            if (parameters.Length == 3)
            {
                secondNewValue = parameters[2];

            }

            var car = this.carService.GetCar(id);

            Brand newBrand = unitOfWork.GetRepository<Brand>().All().FirstOrDefault(b => b.Name == newValue);

            if (newBrand == null)
            {
                newBrand = new Brand() { Name = newValue };
            }
            car.Brand = newBrand;
            unitOfWork.SaveChanges();

        }

        public void EditModel(string[] parameters)
        {
            var id = int.Parse(parameters[0]);
            var newValue = parameters[1];

            var car = this.carService.GetCar(id);
            car.Model = newValue;
            unitOfWork.SaveChanges();
        }

        public void EditHorsePower(string[] parameters)
        {
            var id = int.Parse(parameters[0]);
            var newValue = parameters[1];

            var car = this.carService.GetCar(id);
            car.HorsePower = short.Parse(newValue);
            unitOfWork.SaveChanges();
        }

        public void EditEngineCapacity(string[] parameters)
        {
            var id = int.Parse(parameters[0]);
            var newValue = parameters[1];

            var car = this.carService.GetCar(id);
            car.EngineCapacity = short.Parse(newValue);
            unitOfWork.SaveChanges();
        }

        public void EditIsSold(string[] parameters)
        {
            var id = int.Parse(parameters[0]);
            var newValue = parameters[1];

            var car = this.carService.GetCar(id);
            car.IsSold = bool.Parse(newValue);
            unitOfWork.SaveChanges();
        }

        public void EditPrice(string[] parameters)
        {
            var id = int.Parse(parameters[0]);
            var newValue = parameters[1];

            var car = this.carService.GetCar(id);
            car.Price = decimal.Parse(newValue);
            unitOfWork.SaveChanges();
        }

        public void EditProductionDate(string[] parameters)
        {
            var id = int.Parse(parameters[0]);
            var newValue = parameters[1];

            var car = this.carService.GetCar(id);
            car.ProductionDate = DateTime.Parse(newValue);
            unitOfWork.SaveChanges();
        }

        public void EditBodyType(string[] parameters)
        {
            var id = int.Parse(parameters[0]);
            var newValue = parameters[1];

            var newBodyType = this.unitOfWork.GetRepository<BodyType>().All().FirstOrDefault(ch => ch.Name == newValue);

            if (newBodyType == null)
            {
                throw new ArgumentException("Invalid body type!");
            }

            var car = this.carService.GetCar(id);
            car.BodyType = newBodyType;
            unitOfWork.SaveChanges();
        }

        public void EditColor(string[] parameters)
        {
            var id = int.Parse(parameters[0]);
            var newColorValue = parameters[1];
            var newColorTypeName = "";

            if (parameters.Length == 3)
            {
                newColorTypeName = parameters[2];
            }
            var car = this.carService.GetCar(id);
            
            var newType = unitOfWork.GetRepository<ColorType>().All().FirstOrDefault(gt => gt.Name == newColorTypeName);
            if (newType == null)
            {
                throw new ArgumentException("Invalid color type!");
            }

            var newColor = unitOfWork.GetRepository<Color>().All()
                                     .Include(c => c.ColorType)
                                     .FirstOrDefault(c => c.Name == newColorValue
                                     && c.ColorType.Name == newColorTypeName);

            if (newColor == null)
            {
                newColor = new Color() { Name = newColorValue, ColorType = newType };

                unitOfWork.GetRepository<Color>().Add(newColor);

                this.unitOfWork.SaveChanges();
            }

            car.ColorId = newColor.Id;
            unitOfWork.SaveChanges();
        }

        public void EditColorType(string[] parameters)
        {
            var id = int.Parse(parameters[0]);
            var newValue = parameters[1];

            var car = this.carService.GetCar(id);
            var colorName = car.Color.Name;
            var newColorType = unitOfWork.GetRepository<ColorType>().All().FirstOrDefault(ct => ct.Name == newValue);
            if (newColorType == null) { throw new ArgumentNullException("Color type not exist!"); }

            var newColor = unitOfWork.GetRepository<Color>().All()
                .Include(c => c.ColorType)
                .FirstOrDefault(c => c.Name == colorName
                && c.ColorType.Name == newValue);
            if (newColor == null)
            {
                newColor = new Color { Name = colorName, ColorType = newColorType };
                this.unitOfWork.GetRepository<Color>().Add(newColor);
                this.unitOfWork.SaveChanges();
            }

            car.Color = newColor;
            car.ColorId = newColor.Id;

            unitOfWork.SaveChanges();
        }

        public void EditFuelType(string[] parameters)
        {
            var id = int.Parse(parameters[0]);
            var newValue = parameters[1];

            var car = this.carService.GetCar(id);

            var newFuelType = unitOfWork.GetRepository<FuelType>().All().FirstOrDefault(ft => ft.Name == newValue);

            if (newFuelType != null)
            {
                car.FuelType = newFuelType;
                unitOfWork.SaveChanges();
            }
            else { throw new ArgumentException($"Fuel type :{newValue} not exist!"); }

        }

        public void EditGearbox(string[] parameters) // works but must include navigation props tables !
        {
            var id = int.Parse(parameters[0]);
            var newValue = parameters[1];

            var car = this.carService.GetCar(id);

            GearType newGearType = unitOfWork.GetRepository<GearType>().All().First(gt => gt.Name == newValue);

            if (newGearType == null)
            {
                throw new ArgumentException($"Gearbox:{newValue} not exist!");
            }
            car.GearBox.GearType = newGearType;
            unitOfWork.SaveChanges();
        }
    }
}
