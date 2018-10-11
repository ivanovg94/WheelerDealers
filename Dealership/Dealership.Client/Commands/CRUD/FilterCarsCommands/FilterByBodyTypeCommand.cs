using Dealership.Client.Commands.Abstract;
using Dealership.Client.ViewModels;
using Dealership.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dealership.Client.Commands.CRUD.FilterCarsCommands
{
    public class FilterByBodyTypeCommand : PrimeCommand
    {
        //filterByBodyType {bodyType}
        public ICarService CarService { get; set; }

        public override string Execute(string[] parameters)
        {
            string bodyType = parameters[0];

            var body = CarService.GetBodyType(bodyType);

            var cars = this.CarService.GetCars("asc")
                .Where(c => c.Chasis == body)
                .Select(c => new CarVM
                {
                    Id = c.Id,
                    BrandName = c.Brand.Name,
                    Model = c.Model,
                    EngineCap = c.EngineCapacity,
                    HorsePower = c.HorsePower,
                    ProductionDate = c.ProductionDate,
                    Price = c.Price,
                    NDoors = c.Chasis.NumberOfDoors,
                    Chassis = c.Chasis.Name,
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
                return $"No cars with body type \"{bodyType}\"";
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
