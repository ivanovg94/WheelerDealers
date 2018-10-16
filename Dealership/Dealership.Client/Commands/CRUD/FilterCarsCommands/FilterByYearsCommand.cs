using Dealership.Client.Commands.Abstract;
using Dealership.Client.ViewModels;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using System;
using System.Linq;
using System.Text;

namespace Dealership.Client.Commands.CRUD.FilterCarsCommands
{
    public class FilterByYearsCommand : Command
    {
        private readonly ICarService carService;

        public FilterByYearsCommand(IUserSession userSession, ICarService carService) : base(userSession)
        {
            this.carService = carService;
        }

        public override string Execute(string[] parameters)
        {
            if (parameters.Length != 2)
            {
                throw new ArgumentException("Invalid parameters.");
            }
            
            if (!DateTime.TryParse("01/01/" + parameters[0], out DateTime yearFrom))
            {
                throw new FormatException("Invalid value for the first year!");
            }

            if (!DateTime.TryParse("31/12/" + parameters[1], out DateTime yearTo))
            {
                throw new FormatException("Invalid value for the second year!");
            }

            if (yearFrom > yearTo)
            {
                throw new ArgumentException("The value of the first year cannot exceed the value of the second year!");
            }

            var cars = this.carService.GetCars("asc")
                .Where(c => c.ProductionDate >= yearFrom && c.ProductionDate <= yearTo)
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
                }).ToList();

            if (!cars.Any())
            {
                return $"There are no cars with year between {yearFrom} and {yearTo}.";
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
