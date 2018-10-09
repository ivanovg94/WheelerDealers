using Dealership.Client.Commands.Abstract;
using Dealership.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Client.Commands.ExtrasCommands
{

    public class AddExtraToCarCommand : PrimeCommand
    {
        public IExtraService ExtraService { get; set; }

        //addExtraToCar carId, extraName
        public override string Execute(string[] parameters)
        {
            var carExtra = this.ExtraService.AddExtraToCar(int.Parse(parameters[0]), parameters[1]);
            return $"Added extra {carExtra.Extra.Name} to car with Id {carExtra.CarId}";
        }
    }
}
