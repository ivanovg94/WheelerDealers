using Dealership.Client.Commands.Abstract;
using Dealership.Client.ViewModels;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using System;
using System.Linq;
using System.Text;

namespace Dealership.Client.Commands.CarCommands.FilterCarsCommands
{
    public class FilterByBodyTypeCommand : Command
    {
        private readonly IBodyTypeService bodyTypeService;
        private readonly ICarService carService;

        public FilterByBodyTypeCommand(IUserSession userSession, IBodyTypeService bodyTypeService, ICarService carService) : base(userSession)
        {
            this.bodyTypeService = bodyTypeService;
            this.carService = carService;
        }

        public override string Execute(string[] parameters)
        {
            if (parameters.Length != 1)
            {
                throw new ArgumentException("Invalid parameters.");
            }

            string bodyType = parameters[0];
            var body = bodyTypeService.GetBodyType(bodyType);

            var cars = this.carService.GetCars("asc")
                .Where(c => c.BodyType == body)
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
                 .ToList();

            if (!cars.Any())
            {
                return $"There are no cars with body type {bodyType}";
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
