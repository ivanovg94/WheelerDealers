using Dealership.Client.Commands.Abstract;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using System;
using System.Linq;

namespace Dealership.Client.Commands.CarCommands.ExtrasCommands
{
    public class GetExtrasForCarCommand : AdminCommand
    {
        private readonly IExtraService extraService;

        public GetExtrasForCarCommand(IUserSession userSession, IExtraService extraService) : base(userSession)
        {
            this.extraService = extraService;
        }

        public override string Execute(string[] parameters)
        {
            base.Execute(parameters);
            if (parameters.Length == 0)
            {
                throw new ArgumentException("Invalid parameters");
            }
            if (!int.TryParse(parameters[0], out int id))
            {
                throw new FormatException("Invalid value for Id!");
            }

            var extras = this.extraService.GetExtrasForCar(id);
            if (extras.Count == 0)
            {
                return "No extras.";
            }
            else { return $"Extras: {string.Join(", ", extras.Select(e => e.Name).ToList())}"; }
        }
    }
}
