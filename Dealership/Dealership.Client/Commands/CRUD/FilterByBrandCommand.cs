using Dealership.Client.Commands.Abstract;
using Dealership.Client.Exceptions;
using Dealership.Client.ViewModels;
using System.Linq;
using System.Text;

namespace Dealership.Client.Commands.CRUD
{
    public class FilterByBrandCommand : PrimeCommand
    {
        public override string Execute(string[] parameters)
        {
            string brandName = parameters[0];

            var brand = this.Context.Brands.FirstOrDefault(b => b.Name == brandName);

            if (brand == null)
            {
                throw new BrandNotFoundException($"There is no brand with name {brandName}.");
            }

            var cars = this.Context.Cars.Where(c => c.Brand == brand)
                                .Select(c => new CarVM
                                {
                                    Id = c.Id,
                                    BrandName = c.Brand.Name,
                                    Model = c.Model,
                                    EngineCap = c.EngineCapacity,
                                    HorsePower = c.HorsePower,
                                    ProductionDate = c.ProductionDate,
                                    Price = c.Price,
                                    NDoors=c.Chasis.NumberOfDoors,
                                    Chassis = c.Chasis.Name,
                                    NDoors = c.Chasis.NumberOfDoors,
                                    Color = c.Color.Name,
                                    ColorType = c.Color.ColorType.Type,
                                    Fuel = c.FuelType.Type,
                                    Gearbox = c.GearBox.GearType.Type,
                                    NumberOfGears = c.GearBox.NumberOfGears,
                                    Extras = c.CarsExtras.Select(ce => ce.Extra.Name).ToList()
                                }).ToList();
            if (!cars.Any())
            {
                return $"No cars with brand {brandName}.";
            }

            var sb = new StringBuilder();

            foreach (var car in cars)
            {
                sb.AppendLine(car.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
