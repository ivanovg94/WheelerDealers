﻿using Dealership.Client.Commands.Abstract;
using Dealership.Client.ViewModels;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using System;
using System.Linq;
using System.Text;

namespace Dealership.Client.Commands.CarCommands.FilterCarsCommands
{
    public class FilterByPriceCommand : Command
    {
        private readonly ICarService carService;

        public FilterByPriceCommand(IUserSession userSession, ICarService carService) : base(userSession)
        {
            this.carService = carService;
        }

        public override string Execute(string[] parameters)
        {
            if (parameters.Length != 2)
            {
                throw new ArgumentException("Invalid parameters.");
            }
            
            if (!int.TryParse(parameters[0], out int priceFrom))
            {
                throw new FormatException("Invalid value for the first price!");
            }

            if (!int.TryParse(parameters[1], out int priceTo))
            {
                throw new FormatException("Invalid value for the second price!");
            }

            if (priceFrom > priceTo)
            {
                throw new ArgumentException("The value of the first price cannot exceed the value of the second price!");
            }

            var cars = this.carService.GetCars("asc")
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

            if (cars.Count == 0)
            {
                return $"There are no cars with price between {priceFrom} and {priceTo}.";
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