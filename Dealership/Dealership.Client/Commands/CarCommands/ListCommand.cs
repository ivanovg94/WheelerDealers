using Dealership.Client.Commands.Abstract;
using Dealership.Client.ViewModels;
using Dealership.Data.Models;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dealership.Client.Commands.CarCommands
{
    public class ListCommand : Command
    {
        private readonly ICarService carService;

        public ListCommand(IUserSession userSession, ICarService service) : base(userSession)
        {
            this.carService = service;
        }

        public override string Execute(string[] parameters)
        {
            if (parameters.Length == 0)
            {
                throw new ArgumentException("Invalid parameters");
            }

            IList<Car> data = new List<Car>();
            var dir = "";
            if (parameters.Length == 2)
            {
                dir = parameters[1];
            }
            if (parameters[0].ToLower() == "sold")
            {
                data = carService.GetCars(true, dir);
            }
            else if (parameters[0].ToLower() == "active")
            {
                data = carService.GetCars(false, dir);
            }
            else if (parameters[0].ToLower() == "all")
            {
                data = carService.GetCars(dir);
            }
            else
            {
                throw new ArgumentException("Invalid parameters!");
            }

            var result = data.Select(c => new CarVM
            {
                Id = c.Id,
                BrandName = c.Brand.Name,
                Model = c.Model,
                EngineCap = c.EngineCapacity,
                HorsePower = c.HorsePower,
                ProductionDate = c.ProductionDate,
                Price = c.Price,
                BodyType = c.BodyType.Name,
                NDoors = c.BodyType.NumberOfDoors,
                Color = c.Color.Name,
                ColorType = c.Color.ColorType.Name,
                Fuel = c.FuelType.Name,
                Gearbox = c.GearBox.GearType.Name,
                NumberOfGears = c.GearBox.NumberOfGears,
                Extras = c.CarsExtras.Select(ce => ce.Extra.Name).ToList()
            }).ToList();

            if (result.Count == 0)
            {
                return $"There are no cars to be listed! Create new or inport cars.";
            }
            return string.Join($"\r\n", result);
        }
    }
}
