using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dealership.Client.Commands.Abstract;
using Dealership.Data.Context;
using Dealership.Services;
using Dealership.Services.Abstract;

namespace Dealership.Client.Commands.CRUD
{
    public class ViewCarDetailsCommand : PrimeCommand
    {
        public ICarService CarService { get; set; }
        public ViewCarDetailsCommand()
        {
        }

        // id
        public override string Execute(string[] parameters)
        {
            var car = CarService.GetCar(int.Parse(parameters[0]));
            var extras = string.Join(", ", car.CarsExtras.Select(ce => ce.Extra.Name).ToList());
            return $"Id:{car.Id} {car.Brand.Name} {car.Model}, Engine: {car.EngineCapacity}cc {car.FuelType.Name} {car.HorsePower}hp, Body type {car.Chasis.NumberOfDoors} door {car.Chasis.Name}, Prod.: {car.ProductionDate.ToShortDateString()}, Price: {car.Price}, Color: {car.Color.Name} {car.Color.ColorType.Name} Transmission: {car.GearBox.NumberOfGears} step {car.GearBox.GearType.Name} \r\nExtras: {extras}\r\n";
        }
    }
}
