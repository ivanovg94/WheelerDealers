using Dealership.Client.Commands.Abstract;
using Dealership.Client.Core.Abstract;
using Dealership.Data.Context;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dealership.Client.Commands.CRUD
{
    public class RemoveCarCommand : PrimeCommand
    {
        public RemoveCarCommand(IUserSession userSession) : base(userSession)
        {
        }

        //remove {carId}
        public ICarService CarService { get; set; }

        public override string Execute(string[] parameters)
        {
            int carId = int.Parse(parameters[0]);
            this.CarService.RemoveCar(carId);

            return $"Car with ID {carId} was removed!";
        }
    }
}
