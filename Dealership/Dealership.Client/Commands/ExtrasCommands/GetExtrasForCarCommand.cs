using Dealership.Client.Commands.Abstract;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dealership.Client.Commands.CRUD.ExtrasCommands
{
    public class GetExtrasForCarCommand : PrimeCommand
    {
        public GetExtrasForCarCommand(IUserSession userSession) : base(userSession)
        {
        }

        public IExtraService ExtraService { get; set; }

        //getextrasforcar id
        public override string Execute(string[] parameters)
        {
            if (parameters.Length == 0) { throw new ArgumentException("Invalid parameters"); }
            if (!int.TryParse(parameters[0], out int id)) { throw new ArgumentException("Invalid value for Id!"); }

            var extras = this.ExtraService.GetExtrasForCar(id);
            if (extras.Count == 0) { return "No extras."; }
            else { return $"Extras: {string.Join(", ", extras.Select(e => e.Name).ToList())}"; }
        }
    }
}
