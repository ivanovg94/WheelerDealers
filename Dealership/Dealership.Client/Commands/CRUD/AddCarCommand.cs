using Dealership.Client.Commands.Abstract;
using Dealership.Client.Core.Abstract;
using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services;
using Dealership.Services.Abstract;
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
            short paramHp; //short.Parse(parameters[2]);
            if (!short.TryParse(parameters[2], out paramHp))
            {
                throw new ArgumentException("Invalid horse power value!");
            }

            short paramEngCapacity;
            if (short.TryParse(parameters[3],out paramEngCapacity))
            {
                throw new ArgumentException("Invalid engine capacity value!");
            }

            DateTime paramProdDate;// = DateTime.Parse(parameters[4]);
            if (!DateTime.TryParse(parameters[4],out paramProdDate))
            {
                throw new ArgumentException("Invalid production date passed!");
            }

            decimal paramPrice;//= decimal.Parse(parameters[5]);
            if (!decimal.TryParse(parameters[5],out paramPrice))
            {
                throw new ArgumentException("Invalid price value!");
            }
            var paramChasis = parameters[6];
            var paramColorName = parameters[7];
            var paramColorType = parameters[8];
            var paramFuelType = parameters[9];
            var paramGearbox = parameters[10];

            byte paramNumberOfGears;// = byte.Parse(parameters[11]);
            if (!byte.TryParse(parameters[11],out paramNumberOfGears))
            {
                throw new ArgumentException("Invalid number of gears passed!");
            }

            Brand newBrand = null;

            Chassis newChassis = null;

            Color newColor = null;

            FuelType newFuelType = null;

            Gearbox newGearbox = null;

            if (base.Context.Brands.Any(b => b.Name == paramBrand))
            {
                newBrand = base.Context.Brands.First(b => b.Name == paramBrand);
            }
            else
            {
                newBrand = new Brand() { Name = paramBrand };
                base.Context.Brands.Add(newBrand);
            }

            if (base.Context.Colors.Where(c => c.Name == paramColorName).Any(ct => ct.Name == paramColorType))
            {
                newColor = base.Context.Colors.Where(c => c.Name == paramColorName)
                                              .First(ct => ct.ColorType.Type == paramColorType);
            }
            else
            {
                newColor = new Color()
                {
                    Name = paramColorName,
                    ColorType = base.Context.ColorTypes.First(ct => ct.Type == paramColorType)
                };
            }

            if (base.Context.Chassis.Any(ch => ch.Name == paramChasis))
            {
                base.Context.Chassis.First(ch => ch.Name == paramChasis);
            }
            else
            {
                throw new ArgumentException("Input chassis not valid!");
            }

            if (base.Context.FuelTypes.Any(ft => ft.Type == paramFuelType))
            {
                newFuelType = base.Context.FuelTypes.First(ft => ft.Type == paramFuelType);
            }
            else
            {
                throw new ArgumentException("Input chassis not valid!");
            }

            if (base.Context.Gearboxes.Any(gb => gb.GearType.Type == paramGearbox))
            {
                newGearbox = base.Context.Gearboxes.Where(gb => gb.GearType.Type == paramGearbox)
                  .First(g => g.NumberOfGears == paramNumberOfGears);
            }
            else
            {

                throw new ArgumentException("Entered invalid gearbox!");
            }

            Car commandResult = carService.AddCar(newBrand, paramModel, paramHp
                                                , paramEngCapacity, paramProdDate, paramPrice
                                                , newChassis, newColor, newFuelType, newGearbox);

            return $"{commandResult.Brand.Name} {commandResult.Model} with Id:{commandResult.Id} was added successfully";
        }
    }
}
