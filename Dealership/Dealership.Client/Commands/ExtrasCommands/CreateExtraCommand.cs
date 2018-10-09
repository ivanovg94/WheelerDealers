using Dealership.Client.Commands.Abstract;
using Dealership.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Client.Commands.ExtrasCommands
{
    public class CreateExtraCommand : PrimeCommand
    {
        public IExtraService ExtraService { get; set; }

        //createExtra | name
        public override string Execute(string[] parameters)
        {
            if (parameters.Length == 0) { throw new ArgumentException("Invalid parameters"); }
            var extra = this.ExtraService.CreateExtra(parameters[0]);
            return $"Created extra {extra.Name} with Id {extra.Id}";
        }
    }
}
