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
            if (parameters.Length == 0) { throw new ArgumentException("Invalid parameters"); }
            if (!int.TryParse(parameters[0], out int id)) { throw new ArgumentException("Invalid value for Id!"); }

            var extrasNames = string.Join(" ", parameters.Skip(1));

            if (string.IsNullOrEmpty(extrasNames)) { throw new ArgumentException("c ne baca"); }
            var extra = this.ExtraService.AddExtraToCar(id, extrasNames);
            return $"Added extra {extra.Name} to car with Id {id}";
        }
    }
}
