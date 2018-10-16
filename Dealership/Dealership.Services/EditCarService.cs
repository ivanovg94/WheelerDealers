using Dealership.Data.Models;
using Dealership.Data.UnitOfWork;
using Dealership.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Dealership.Services
{
    public class EditCarService : IEditCarService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICarService carService;
        // test purposes
        public ICarService CarService => carService;
        public IUnitOfWork UnitOfWork => unitOfWork;


        //mocking purposes
        public EditCarService()
        {

        }

        public EditCarService(IUnitOfWork unitOfWork, ICarService carService)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("UnitOfWork cannot be null!");
            }
            if (carService == null)
            {
                throw new ArgumentNullException("CarService cannot be null!");
            }
            this.unitOfWork = unitOfWork;
            this.carService = carService;
            //field init changed to prop for testing
        }

        public virtual string EditBrand(string[] parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("Parameters cannot be null!");
            }
            if (parameters.Length == 0)
            {
                throw new ArgumentException("Invalid number of parameters!");
            }

            int id;
            if (!int.TryParse(parameters[0], out id))
            {
                throw new ArgumentException("Invalid ID!");
            }

            string newValue = parameters[1];

            string secondNewValue = "";
            if (parameters.Length == 3)
            {
                secondNewValue = parameters[2];
            }

            var car = this.CarService.GetCar(id);

            Brand newBrand = UnitOfWork.GetRepository<Brand>().All().FirstOrDefault(b => b.Name == newValue);

            if (newBrand == null)
            {
                newBrand = new Brand() { Name = newValue };
            }
            car.Brand = newBrand;
            UnitOfWork.SaveChanges();

            return $"Brand of {car.Brand.Name} {car.Model} with ID:{car.Id} edited successfully!";

        }

        public string EditModel(string[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                throw new ArgumentNullException("Invalid amount of parameters!");
            }
            int id /*int.Parse(parameters[0])*/;
            if (!int.TryParse(parameters[0], out id))
            {
                throw new ArgumentException("Invalid ID!");
            }
            var newValue = parameters[1];

            var car = this.CarService.GetCar(id);
            car.Model = newValue;
            UnitOfWork.SaveChanges();

            return $"Model of {car.Brand.Name} {car.Model} with ID:{car.Id} edited successfully!";
        }

        public string EditHorsePower(string[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                throw new ArgumentNullException("Invalid amount of parameters!");
            }

            int id /*int.Parse(parameters[0])*/;
            if (!int.TryParse(parameters[0], out id))
            {
                throw new ArgumentException("Invalid ID!");
            }
            var newValue = parameters[1];

            var car = this.CarService.GetCar(id);
            car.HorsePower = short.Parse(newValue);
            UnitOfWork.SaveChanges();

            return $"Horse power of {car.Brand.Name} {car.Model} with ID:{car.Id} edited successfully!";
        }

        public string EditEngineCapacity(string[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                throw new ArgumentNullException("Invalid amount of parameters!");
            }

            int id /*int.Parse(parameters[0])*/;
            if (!int.TryParse(parameters[0], out id))
            {
                throw new ArgumentException("Invalid ID!");
            }

            var newValue = parameters[1];

            var car = this.CarService.GetCar(id);
            car.EngineCapacity = short.Parse(newValue);
            UnitOfWork.SaveChanges();

            return $"Engine capacity of {car.Brand.Name} {car.Model} with ID:{car.Id} edited successfully!";
        }

        public string EditIsSold(string[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                throw new ArgumentNullException("Invalid amount of parameters!");
            }

            int id /*int.Parse(parameters[0])*/;
            if (!int.TryParse(parameters[0], out id))
            {
                throw new ArgumentException("Invalid ID!");
            }
           
            var newValue = parameters[1];

            var car = this.CarService.GetCar(id);
            car.IsSold = bool.Parse(newValue);
            UnitOfWork.SaveChanges();

            return $"IsSold of {car.Brand.Name} {car.Model} with ID:{car.Id} edited successfully!";
        }

        public string EditPrice(string[] parameters)// not tested
        {
            if (parameters == null || parameters.Length == 0)
            {
                throw new ArgumentNullException("Invalid amount of parameters!");
            }

            int id /*int.Parse(parameters[0])*/;
            if (!int.TryParse(parameters[0], out id))
            {
                throw new ArgumentException("Invalid ID!");
            }
            var newValue = parameters[1];

            var car = this.CarService.GetCar(id);
            car.Price = decimal.Parse(newValue);
            UnitOfWork.SaveChanges();

            return $"Price of {car.Brand.Name} {car.Model} with ID:{car.Id} edited successfully!";
        }

        public string EditProductionDate(string[] parameters)// not tested
        {
            if (parameters == null || parameters.Length == 0)
            {
                throw new ArgumentNullException("Invalid amount of parameters!");
            }

            int id /*int.Parse(parameters[0])*/;
            if (!int.TryParse(parameters[0], out id))
            {
                throw new ArgumentException("Invalid ID!");
            }
            var newValue = parameters[1];

            var car = this.CarService.GetCar(id);
            car.ProductionDate = DateTime.Parse(newValue);
            UnitOfWork.SaveChanges();

            return $"Production date of {car.Brand.Name} {car.Model} with ID:{car.Id} edited successfully!";
        }

        public string EditBodyType(string[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                throw new ArgumentNullException("Invalid amount of parameters!");
            }

            int id /*int.Parse(parameters[0])*/;
            if (!int.TryParse(parameters[0], out id))
            {
                throw new ArgumentException("Invalid ID!");
            }
            var newValue = parameters[1];

            var newBodyType = this.UnitOfWork.GetRepository<BodyType>().All().FirstOrDefault(ch => ch.Name == newValue);

            if (newBodyType == null)
            {
                throw new ArgumentException("Invalid body type!");
            }

            var car = this.CarService.GetCar(id);
            car.BodyType = newBodyType;
            UnitOfWork.SaveChanges();

            return $"Body type of {car.Brand.Name} {car.Model} with ID:{car.Id} edited successfully!";
        }

        public string EditColor(string[] parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("Parameters cannot be null!");
            }
            if (parameters.Length == 0)
            {
                throw new ArgumentException("Invalid number of parameters!");
            }

            int id;
            if (!int.TryParse(parameters[0], out id))
            {
                throw new ArgumentException("Invalid ID!");
            }
            var newColorValue = parameters[1];
            string newColorTypeName = "";

            if (parameters.Length == 3)
            {
                newColorTypeName = parameters[2];
            }
            var car = this.CarService.GetCar(id);

            var newType = UnitOfWork.GetRepository<ColorType>().All().FirstOrDefault(gt => gt.Name == newColorTypeName);
            if (newType == null)
            {
                throw new ArgumentException("Invalid color type!");
            }

            var newColor = UnitOfWork.GetRepository<Color>().All()
                                     .Include(c => c.ColorType)
                                     .FirstOrDefault(c => c.Name == newColorValue
                                     && c.ColorType.Name == newColorTypeName);

            if (newColor == null)
            {
                newColor = new Color() { Name = newColorValue, ColorType = newType };

                UnitOfWork.GetRepository<Color>().Add(newColor);

                this.UnitOfWork.SaveChanges();
            }

            car.ColorId = newColor.Id;
            UnitOfWork.SaveChanges();

            return $"Color of {car.Brand.Name} {car.Model} with ID:{car.Id} edited successfully!";
        }

        public string EditColorType(string[] parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("Parameters cannot be null!");
            }
            if (parameters.Length == 0)
            {
                throw new ArgumentException("Invalid number of parameters!");
            }

            int id;
            if (!int.TryParse(parameters[0], out id))
            {
                throw new ArgumentException("Invalid ID!");
            }
            var newValue = parameters[1];

            var car = this.CarService.GetCar(id);
            var colorName = car.Color.Name;
            var newColorType = UnitOfWork.GetRepository<ColorType>().All().FirstOrDefault(ct => ct.Name == newValue);
            if (newColorType == null) { throw new ArgumentNullException("Color type not exist!"); }

            var newColor = UnitOfWork.GetRepository<Color>().All()
                .Include(c => c.ColorType)
                .FirstOrDefault(c => c.Name == colorName
                && c.ColorType.Name == newValue);
            if (newColor == null)
            {
                newColor = new Color { Name = colorName, ColorType = newColorType };
                this.UnitOfWork.GetRepository<Color>().Add(newColor);
                this.UnitOfWork.SaveChanges();
            }

            car.Color = newColor;
            car.ColorId = newColor.Id;

            UnitOfWork.SaveChanges();

            return $"Color type of {car.Brand.Name} {car.Model} with ID:{car.Id} edited successfully!";
        }

        public string EditFuelType(string[] parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("Parameters cannot be null!");
            }
            if (parameters.Length == 0)
            {
                throw new ArgumentException("Invalid number of parameters!");
            }

            int id;
            if (!int.TryParse(parameters[0], out id))
            {
                throw new ArgumentException("Invalid ID!");
            }
            var newValue = parameters[1];

            var car = this.CarService.GetCar(id);

            var newFuelType = UnitOfWork.GetRepository<FuelType>().All().FirstOrDefault(ft => ft.Name == newValue);

            if (newFuelType != null)
            {
                car.FuelType = newFuelType;
                UnitOfWork.SaveChanges();
            }
            else
            {
                throw new ArgumentException($"Fuel type :{newValue} not exist!");
            }

            return $"Fuel type of {car.Brand.Name} {car.Model} with ID:{car.Id} edited successfully!";

        }

        public string EditGearbox(string[] parameters) // works but must include navigation props tables !
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("Parameters cannot be null!");
            }
            if (parameters.Length == 0)
            {
                throw new ArgumentException("Invalid number of parameters!");
            }

            int id;
            if (!int.TryParse(parameters[0], out id))
            {
                throw new ArgumentException("Invalid ID!");
            }
            var newValue = parameters[1];

            var car = this.CarService.GetCar(id);

            GearType newGearType = UnitOfWork.GetRepository<GearType>().All().First(gt => gt.Name == newValue);

            if (newGearType == null)
            {
                throw new ArgumentException($"Gearbox:{newValue} not exist!");
            }
            car.GearBox.GearType = newGearType;
            UnitOfWork.SaveChanges();

            return $"Gearbox of {car.Brand.Name} {car.Model} with ID:{car.Id} edited successfully!";
        }
    }
}
