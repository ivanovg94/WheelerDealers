using Dealership.Client.Commands.Abstract;
using Dealership.Client.Core.Abstract;
using Dealership.Data.Context;
using Dealership.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dealership.Client.Commands.CRUD
{
    public class EditCommand : PrimeCommand
    {
        // edit [id]
        public override string Execute(string[] parameters)
        {
            throw new NotImplementedException();

            if (parameters.Length == 1)
            {
                // list all car props with old value and ask for new value
                var id = int.Parse(parameters[0]);
                var currentCar = base.Context.Cars
                    .Include(b => b.Brand)
                    .Include(ch => ch.Chasis)
                    .Include(c => c.Color)
                    .Include(f => f.FuelType)
                    .Include(gb => gb.GearBox)
                    .Include(x => x.CarsExtras)
                    .First(c => c.Id == id);
                //currentCar.Brand = base.Context.Brands(currentCar.BrandId);

                Console.Write($"Brand (old {currentCar.Brand.Name}) => ");
                var newValue = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newValue))
                {
                    if (!base.Context.Brands.Any(b => b.Name == newValue))
                    {
                        base.Context.Brands.Add(new Brand() { Name = newValue });
                    }
                    currentCar.BrandId = base.Context.Brands.First(b => b.Name == newValue).Id;
                }

                Console.Write($"Model (current {currentCar.Model}) => ");
                newValue = Console.ReadLine();
                Console.WriteLine();
                if (!string.IsNullOrWhiteSpace(newValue))
                {
                    currentCar.Model = newValue;
                }

                Console.Write($"Horse power (current {currentCar.HorsePower}) => ");
                var shortValue = short.Parse(Console.ReadLine());
                Console.WriteLine();
                if (!string.IsNullOrWhiteSpace(newValue))
                {
                    currentCar.HorsePower = shortValue;
                }

                Console.Write($"Engine capacity (current {currentCar.EngineCapacity}) => ");
                var engCapacityValue = short.Parse(Console.ReadLine());
                Console.WriteLine();
                if (!string.IsNullOrWhiteSpace(newValue))
                {
                    currentCar.EngineCapacity = engCapacityValue;
                }

                Console.Write($"Production date (current {currentCar.ProductionDate}) => ");
                var newProdDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine();
                if (!string.IsNullOrWhiteSpace(newValue))
                {
                    currentCar.ProductionDate = newProdDate;
                }

                Console.Write($"Price (current {currentCar.Price}) => ");
                var newPrice = decimal.Parse(Console.ReadLine());
                Console.WriteLine();
                if (!string.IsNullOrWhiteSpace(newValue))
                {
                    currentCar.Price = newPrice;
                }

                //Console.Write($"Chassis (current {currentCar.Chasis.Name}) => ");
                //var newChassisId = Console.ReadLine();
                //Console.WriteLine();
                //if (!string.IsNullOrWhiteSpace(newValue) && base.Context.Chassis.Contains(id))
                //{
                //    currentCar.ChasisId = newChassisId;
                //}

                //Console.Write($"Color (current {currentCar.Color}) => ");
                //var newColorId = int.Parse(Console.ReadLine());
                //Console.WriteLine();
                //if (!string.IsNullOrWhiteSpace(newValue))
                //{
                //    currentCar.ChasisId = newColorId;
                //}

                base.Context.SaveChanges();
                return $"Car with Id {currentCar.Id} edited successfully!";
                // if null(hit enter key) value is passed didn't edit prop value
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
