using Dealership.Client.Commands.Abstract;
using Dealership.Client.Core.Abstract;
using Dealership.Client.ViewModels;
using Dealership.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dealership.Client.Commands.CRUD
{
    public class ListCommand : PrimeCommand
    {
        public ListCommand()
        {
        }

        // null/sold direction
        public override string Execute(string[] parameters)
        {
            bool isSold = false;
            if (parameters.Length == 2)
            {
                isSold = true;
            }

            var querry = base.Context.Cars.Where(c => c.IsSold == isSold)
                                .Select(c => new CarVM
                                {
                                    Id = c.Id,
                                    BrandName = c.Brand.Name,
                                    Model = c.Model,
                                    EngineCap = c.EngineCapacity,
                                    HorsePower = c.HorsePower,
                                    ProductionDate = c.ProductionDate,
                                    Price = c.Price,
                                    Chassis = c.Chasis.Name,
                                    NDoors = c.Chasis.NumberOfDoors,
                                    Color = c.Color.Name,
                                    ColorType = c.Color.ColorType.Type,
                                    Fuel = c.FuelType.Type,
                                    Gearbox = c.GearBox.GearType.Type,
                                    NumberOfGears = c.GearBox.NumberOfGears,
                                    Extras = c.CarsExtras.Select(ce => ce.Extra.Name).ToList()
                                });
            var data = new List<CarVM>();

            if (parameters.Last() == "ASC")
            {
                data = querry.OrderBy(c => c.Id).ToList();
            }
            else if (parameters.Last() == "DESC")
            {
                data = querry.OrderByDescending(c => c.Id).ToList();
            }

            return string.Join($"{new string('-', 151)}\r\n", data);
        }
    }
}
