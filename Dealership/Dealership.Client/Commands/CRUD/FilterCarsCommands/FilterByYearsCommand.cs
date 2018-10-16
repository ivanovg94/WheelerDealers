﻿using Dealership.Client.Commands.Abstract;
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
            string yearFromInput = parameters[0];
            string yearToInput = parameters[1];

            DateTime yearFrom = DateTime.Parse("01/01/" + yearFromInput);
            DateTime yearTo = DateTime.Parse("31/12/" + yearToInput);


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
                return $"No cars with year between {yearFromInput} and {yearToInput}.";
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
