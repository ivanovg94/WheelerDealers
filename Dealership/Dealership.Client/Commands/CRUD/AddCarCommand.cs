using Dealership.Client.Commands.Abstract;
using Dealership.Client.Core.Abstract;
using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dealership.Client.Commands.CRUD
{
    public class AddCarCommand : PrimeCommand
    {
        public ICarService carService { get; set; }
        //add brand, model, hp, engCap, prod.date, price, chasis, nDoors, colorName,ColorType, fuelType, gearbox, nGears
        public override string Execute(string[] parameters)
        {
            // validation TODO
            var paramBrand = parameters[0];
            var paramModel = parameters[1];
            var paramHp = short.Parse(parameters[2]);
            var paramEngCapacity = short.Parse(parameters[3]);
            var paramProdDate = DateTime.Parse(parameters[4]);
            var paramPrice = decimal.Parse(parameters[5]);
            var paramChasis = parameters[6];
            var paramColorName = parameters[7];
            var paramColorType = parameters[8];
            var paramFuelType = parameters[9];
            var paramGearbox = parameters[10];
            var paramNumberOfGears = byte.Parse(parameters[11]);

            Brand newBrand = null;

            Chassis newChassis = base.Context.Chassis.First(ch => ch.Name == paramChasis);

            Color newColor = null;

            FuelType newFuelType = base.Context.FuelTypes.First(ft => ft.Type == paramFuelType);
            Gearbox newGearbox = base.Context.Gearboxes.Where(gb => gb.GearType.Type == paramGearbox).First(g => g.NumberOfGears == paramNumberOfGears);

            if (!base.Context.Brands.Any(b => b.Name == paramBrand))
            {
                newBrand = new Brand() { Name = paramBrand };
                base.Context.Brands.Add(newBrand);
            }
            else
            {
                newBrand = base.Context.Brands.First(b => b.Name == paramBrand);

            }

            if (!base.Context.Colors.Where(c => c.Name == paramColorName).Any(ct => ct.Name == paramColorType))
            {
                newColor = new Color()
                {
                    Name = paramColorName,
                    ColorType = base.Context.ColorTypes.First(ct => ct.Type == paramColorType)
                };
            }
            else
            {
                newColor = base.Context.Colors.Where(c => c.Name == paramColorName)
                                              .First(ct => ct.ColorType.Type == paramColorType);
            }

            Car commandResult = carService.AddCar(newBrand, paramModel, paramHp
                 , paramEngCapacity, paramProdDate, paramPrice
                 , newChassis, newColor, newFuelType, newGearbox);

            return $"{commandResult.Brand.Name} {commandResult.Model} with Id:{commandResult.Id} was added successfully";
        }
    }
}
