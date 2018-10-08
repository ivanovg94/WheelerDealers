using Dealership.Client.Commands.Abstract;
using Dealership.Client.Core.Abstract;
using Dealership.Client.Exceptions;
using Dealership.Data.Context;
using Dealership.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dealership.Client.Commands.CRUD
{
    public class RemoveCarCommand : PrimeCommand
    {
        //remove {carId}
        public ICarService CarService { get; set; }

        public override string Execute(string[] parameters)
        {
            int carId = int.Parse(parameters[0]);

            var car = this.CarService.GetCar(carId);

            this.Context.Cars.Remove(car);
            base.Context.SaveChanges();

            return $"Car with ID {carId} was removed!";
        }
    }
}
