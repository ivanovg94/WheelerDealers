using Dealership.Data.Models;
using Dealership.Data.Repository;
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
        private readonly IRepository<Car> carsRepository;
        private readonly IRepository<Brand> brandRepository;
        private readonly IRepository<Chassis> chassisRepository;
        private readonly IRepository<Color> colorRepository;
        private readonly IRepository<ColorType> colorTypeRepository;
        private readonly IRepository<FuelType> fuelTypeRepository;
        private readonly IRepository<Gearbox> gearboxRepository;
        private readonly IRepository<GearType> gearTypeRepository;
               
        public CarService(IRepository<Car> carsRepository, IRepository<Brand> brandRepository, IRepository<Chassis> chassisRepository, IRepository<Color> colorRepository, IRepository<ColorType> colorTypeRepository, IRepository<FuelType> fuelTypeRepository, IRepository<Gearbox> gearboxRepository, IRepository<GearType> gearTypeRepository)
        {
            this.carsRepository = carsRepository;
            this.brandRepository = brandRepository;
            this.chassisRepository = chassisRepository;
            this.colorRepository = colorRepository;
            this.colorTypeRepository = colorTypeRepository;
            this.fuelTypeRepository = fuelTypeRepository;
            this.gearboxRepository = gearboxRepository;
            this.gearTypeRepository = gearTypeRepository;
        }

        public Car CreateCar(string brandName, string model, short horsePower, short engineCapacity
           , DateTime productionDate, decimal price, string chassisName, string colorName, string colorType, string fuelTypeName, string gearboxTypeName, int numOfGears)
        {
            var brand = this.brandRepository.All().FirstOrDefault(b => b.Name == brandName);

            if (brand == null)
            {
                brand = new Brand() { Name = brandName };
                this.brandRepository.Add(brand);
                this.brandRepository.Save();
            }

            var chassis = this.chassisRepository.All().FirstOrDefault(c => c.Name == chassisName);
            if (chassis == null)
            {
                throw new ChassisNotFoundException($"There is no chassis with name \"{chassisName}\".");
            }

            var color = this.colorRepository.All().FirstOrDefault(c => c.Name == colorName);
            if (color == null)
            {
                color = new Color
                {
                    Name = colorName,
                    ColorType = this.colorTypeRepository.All().FirstOrDefault(ct => ct.Name == colorType)
                };

                if (color.ColorType == null)
                {
                    throw new ColorTypeNotFoundException($"There is no color type with name \"{chassisName}\".");
                }
                this.colorRepository.Add(color);
                this.colorRepository.Save();
            }

            var fuelType = this.fuelTypeRepository.All().FirstOrDefault(f => f.Name == fuelTypeName);
            if (fuelType == null)
            {
                throw new FuelNotFoundException($"There is no fuel with name \"{fuelTypeName}\".");
            }

            var gearbox = this.gearboxRepository.All().FirstOrDefault(g => g.GearType.Name == gearboxTypeName && g.NumberOfGears == numOfGears);
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
            this.carsRepository.Add(car);
            this.carsRepository.Save();

            return car;
        }

        public void AddCars(ICollection<Car> cars)
        {
            foreach (var car in cars)
            {
                this.carsRepository.Add(car);
            }

            this.carsRepository.Save();
        }

        public IList<Car> GetCars(bool filterSold, string direction)
        {
            var querry = this.carsRepository.All()
                                            .Where(c => c.IsSold == filterSold)
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
            else { return querry.ToList(); }
        }

        public IList<Car> GetCars(string direction)
        {
            var querry = this.carsRepository.All()
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
            else { return querry.ToList(); }
        }

        public Car GetCar(int id)
        {
            var car = this.carsRepository.All().Where(c => c.Id == id)
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

            this.carsRepository.Delete(car);
            this.carsRepository.Save();

            return car;
        }

        public void EditBrand(int id, string newValue) // works but must include navigation props tables !
        {
            Car car = carsRepository.All().Include(b => b.Brand)
                    .Include(ch => ch.Chasis)
                    .Include(c => c.Color)
                    .Include(f => f.FuelType)
                    .Include(gb => gb.GearBox)
                    .Include(x => x.CarsExtras)
                    .First(c => c.Id == id);

            if (car != null)
            {
                Brand newBrand;
                if (brandRepository.All().Any(b => b.Name == newValue))
                {
                    newBrand = brandRepository.All().First(b => b.Name == newValue);
                }
                else
                {
                    newBrand = new Brand() { Name = newValue };
                }
                car.Brand = newBrand;
                brandRepository.Save();
            }
            else
            {
                throw new ArgumentNullException($"Car with id:{id} not exist!");
            }
        }

        public void EditModel(int id, string newValue) // works but must include navigation props tables !
        {
            Car car = carsRepository.All().Include(b => b.Brand)
                    .Include(ch => ch.Chasis)
                    .Include(c => c.Color)
                    .Include(f => f.FuelType)
                    .Include(gb => gb.GearBox)
                    .Include(x => x.CarsExtras)
                    .First(c => c.Id == id);

            if (car != null)
            {
                car.Model = newValue;
                carsRepository.Save();
            }
            else
            {
                throw new ArgumentNullException($"Car with id:{id} not exist!");
            }
        }

        public void EditHorsePower(int id, string newValue) // works but must include navigation props tables !
        {
            Car car = carsRepository.All().Include(b => b.Brand)
                    .Include(ch => ch.Chasis)
                    .Include(c => c.Color)
                    .Include(f => f.FuelType)
                    .Include(gb => gb.GearBox)
                    .Include(x => x.CarsExtras)
                    .First(c => c.Id == id);

            if (car != null)
            {
                car.HorsePower = short.Parse(newValue);
                carsRepository.Save();
            }
            else
            {
                throw new ArgumentNullException($"Car with id:{id} not exist!");
            }
        }

        public void EditEngineCapacity(int id, string newValue) // works but must include navigation props tables !
        {
            Car car = carsRepository.All().Include(b => b.Brand)
                    .Include(ch => ch.Chasis)
                    .Include(c => c.Color)
                    .Include(f => f.FuelType)
                    .Include(gb => gb.GearBox)
                    .Include(x => x.CarsExtras)
                    .First(c => c.Id == id);

            if (car != null)
            {
                car.EngineCapacity = short.Parse(newValue);
                carsRepository.Save();
            }
            else
            {
                throw new ArgumentNullException($"Car with id:{id} not exist!");
            }
        }

        public void EditIsSold(int id, string newValue) // works but must include navigation props tables !
        {
            Car car = carsRepository.All().Include(b => b.Brand)
                    .Include(ch => ch.Chasis)
                    .Include(c => c.Color)
                    .Include(f => f.FuelType)
                    .Include(gb => gb.GearBox)
                    .Include(x => x.CarsExtras)
                    .First(c => c.Id == id);

            if (car != null)
            {
                car.IsSold = bool.Parse(newValue);
                carsRepository.Save();
            }
            else
            {
                throw new ArgumentNullException($"Car with id:{id} not exist!");
            }
        }

        public void EditPrice(int id, string newValue) // works but must include navigation props tables !
        {
            Car car = carsRepository.All().Include(b => b.Brand)
                    .Include(ch => ch.Chasis)
                    .Include(c => c.Color)
                    .Include(f => f.FuelType)
                    .Include(gb => gb.GearBox)
                    .Include(x => x.CarsExtras)
                    .First(c => c.Id == id);

            if (car != null)
            {
                car.Price = decimal.Parse(newValue);
                carsRepository.Save();
            }
            else
            {
                throw new ArgumentNullException($"Car with id:{id} not exist!");
            }
        }

        public void EditProductionDate(int id, string newValue) // works but must include navigation props tables !
        {
            Car car = carsRepository.All().Include(b => b.Brand)
                    .Include(ch => ch.Chasis)
                    .Include(c => c.Color)
                    .Include(f => f.FuelType)
                    .Include(gb => gb.GearBox)
                    .Include(x => x.CarsExtras)
                    .First(c => c.Id == id);

            if (car != null)
            {
                car.ProductionDate = DateTime.Parse(newValue);
                carsRepository.Save();
            }
            else
            {
                throw new ArgumentNullException($"Car with id:{id} not exist!");
            }
        }

        public void EditChassis(int id, string newValue) // works but must include navigation props tables !
        {
            Car car = carsRepository.All().Include(b => b.Brand)
                    .Include(ch => ch.Chasis)
                    .Include(c => c.Color)
                    .Include(f => f.FuelType)
                    .Include(gb => gb.GearBox)
                    .Include(x => x.CarsExtras)
                    .First(c => c.Id == id);

            if (car != null)
            {
                car.Chasis.Name = newValue;
                carsRepository.Save();
            }
            else
            {
                throw new ArgumentNullException($"Car with id:{id} not exist!");
            }
        }

        public void EditColor(int id, string newValue) // works but must include navigation props tables !
        {
            Car car = carsRepository.All().Include(b => b.Brand)
                    .Include(ch => ch.Chasis)
                    .Include(c => c.Color)
                    .Include(f => f.FuelType)
                    .Include(gb => gb.GearBox)
                    .Include(x => x.CarsExtras)
                    .First(c => c.Id == id);

            if (car != null)
            {
                Color newColor;
                if (colorRepository.All().Any(c => c.Name == newValue))
                {
                    newColor = colorRepository.All().First();
                }
                else
                {
                    newColor = new Color()
                    {
                        Name = newValue
                        ,
                        ColorType = new ColorType() { Name = "Metalic" }
                    };//default type "Metalic"
                    colorRepository.Add(newColor);
                }

                car.Color = newColor;
                carsRepository.Save();
            }
            else
            {
                throw new ArgumentNullException($"Car with id:{id} not exist!");
            }
        }

        public void EditColorType(int id, string newValue) // works but must include navigation props tables !
        {
            Car car = carsRepository.All().Include(b => b.Brand)
                    .Include(ch => ch.Chasis)
                    .Include(c => c.Color)
                    .Include(f => f.FuelType)
                    .Include(gb => gb.GearBox)
                    .Include(x => x.CarsExtras)
                    .First(c => c.Id == id);

            if (car != null)
            {
                ColorType newColorType;
                if (colorTypeRepository.All().Any(c => c.Name == newValue))
                {
                    newColorType = colorTypeRepository.All().First(ct => ct.Name == newValue);
                }
                else
                {
                    throw new ArgumentNullException("Color type not exist!");
                }

                car.Color.ColorType = newColorType;
                carsRepository.Save();
            }
            else
            {
                throw new ArgumentNullException($"Car with id:{id} not exist!");
            }
        }

        public void EditFuelType(int id, string newValue) // works but must include navigation props tables !
        {
            Car car = carsRepository.All().Include(b => b.Brand)
                    .Include(ch => ch.Chasis)
                    .Include(c => c.Color)
                    .Include(f => f.FuelType)
                    .Include(gb => gb.GearBox)
                    .Include(x => x.CarsExtras)
                    .First(c => c.Id == id);

            if (car != null)
            {
                var newFuelType = fuelTypeRepository.All().First(ft => ft.Name == newValue);
                if (newFuelType != null)
                {
                    car.FuelType = newFuelType;
                    fuelTypeRepository.Save();
                }
                else
                {
                    throw new ArgumentException($"Fuel type :{newValue} not exist!");
                }
            }
            else
            {
                throw new ArgumentNullException($"Car with id:{id} not exist!");
            }
        }

        public void EditGearbox(int id, string newValue) // works but must include navigation props tables !
        {
            Car car = carsRepository.All().Include(b => b.Brand)
                    .Include(ch => ch.Chasis)
                    .Include(c => c.Color)
                    .Include(f => f.FuelType)
                    .Include(gb => gb.GearBox)
                    .Include(x => x.CarsExtras)
                    .First(c => c.Id == id);

            if (car != null)
            {
                GearType newGearType;
                if (gearTypeRepository.All().Any(gb => gb.Name == newValue))
                {
                    newGearType = gearTypeRepository.All().First(gt => gt.Name == newValue);
                }
                else
                {
                    throw new ArgumentException($"Gearbox:{newValue} not exist!");
                }
                car.GearBox.GearType = newGearType;
                carsRepository.Save();
            }
            else
            {
                throw new ArgumentNullException($"Car with id:{id} not exist!");
            }
        }

        public Brand GetBrand(string brandName)
        {
            var brand = this.brandRepository.All().FirstOrDefault(b => b.Name == brandName);

            if (brand == null)
            {
                throw new BrandNotFoundException($"There is no brand with name \"{brandName}\".");
            }

            return brand;
        }
    }
}
