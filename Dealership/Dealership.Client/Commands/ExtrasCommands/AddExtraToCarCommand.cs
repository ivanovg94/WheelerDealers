using Dealership.Client.Commands.Abstract;
using Dealership.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dealership.Client.Commands.ExtrasCommands
{

    public class AddExtraToCarCommand : PrimeCommand
    {
        public IExtraService ExtraService { get; set; }

        //addExtraToCar carId, extraName
        public override string Execute(string[] parameters)
        {
            var extra = this.ExtraService.AddExtraToCar(int.Parse(parameters[0]), string.Join(" ",parameters.Skip(1)));
            return $"Added extra {extra.Name} to car with Id {parameters[0]}";
        }
    }
}
