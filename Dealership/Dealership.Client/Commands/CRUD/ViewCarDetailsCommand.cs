using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dealership.Client.Commands.Abstract;
using Dealership.Data.Context;
using Dealership.Data.Models.Contracts;
using Dealership.Services;
using Dealership.Services.Abstract;

namespace Dealership.Client.Commands.CRUD
{
    public class ViewCarDetailsCommand : Command
    {
        private ICarService carService;
        public ViewCarDetailsCommand(IUserSession userSession, ICarService carService) : base(userSession)
        {
            this.carService = carService;
        }

        // id
        public override string Execute(string[] parameters)
        {
            if (parameters.Length == 0) { throw new ArgumentException("Invalid parameters"); }

            string id = parameters[0];
            if (!int.TryParse(id, out int idInt))
            {
                throw new FormatException("Id must be numer!");
            }
            var car = this.carService.GetCar(idInt);
            var extras = string.Join(", ", car.CarsExtras.Select(ce => ce.Extra.Name).ToList());
            return $"Id:{car.Id} {car.Brand.Name} {car.Model}, Engine: {car.EngineCapacity}cc {car.FuelType.Name} {car.HorsePower}hp, Body type {car.BodyType.NumberOfDoors} door {car.BodyType.Name}, Prod.: {car.ProductionDate.ToShortDateString()}, Price: {car.Price}, Color: {car.Color.Name} {car.Color.ColorType.Name} Transmission: {car.GearBox.NumberOfGears} step {car.GearBox.GearType.Name} \r\nExtras: {extras}\r\n";
        }
    }
}
