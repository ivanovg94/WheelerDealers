
using Dealership.Client.Commands.Abstract;
using Dealership.Client.ViewModels;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using System;
using System.Linq;
using System.Text;

namespace Dealership.Client.Commands.CRUD.FilterCarsCommands
{
    public class FilterByPriceCommand : PrimeCommand
    {
        public FilterByPriceCommand(IUserSession userSession) : base(userSession)
        {
        }

        //filterByPrice {priceFrom} {priceTo}
        public ICarService CarService { get; set; }

        public override string Execute(string[] parameters)
        {
            int priceFrom = int.Parse(parameters[0]);
            int priceTo = int.Parse(parameters[1]);

            if (priceFrom > priceTo)
            {
                throw new ArgumentException("The value of the first price cannot exceed the value of the second price!");
            }

            var cars = this.CarService.GetCars("asc")
                .Where(c => c.Price >= priceFrom && c.Price <= priceTo)
                .Select(c => new CarVM
                {
                    Id = c.Id,
                    BrandName = c.Brand.Name,
                    Model = c.Model,
                    EngineCap = c.EngineCapacity,
                    HorsePower = c.HorsePower,
                    ProductionDate = c.ProductionDate,
                    Price = c.Price,
                    NDoors = c.BodyType.NumberOfDoors,
                    BodyType = c.BodyType.Name,
                    Color = c.Color.Name,
                    ColorType = c.Color.ColorType.Name,
                    Fuel = c.FuelType.Name,
                    Gearbox = c.GearBox.GearType.Name,
                    NumberOfGears = c.GearBox.NumberOfGears,
                    Extras = c.CarsExtras.Select(ce => ce.Extra.Name).ToList()
                })
                 .OrderBy(c => c.Price)
                 .ToList();

            if (!cars.Any())
            {
                return $"No cars with price between {priceFrom} and {priceTo}.";
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
