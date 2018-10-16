using Dealership.Client.Commands.Abstract;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using System;
using System.Linq;

namespace Dealership.Client.Commands.CRUD.ExtrasCommands
{
    public class AddExtraToCarCommand : AdminCommand
    {
        private readonly IExtraService extraService;

        public AddExtraToCarCommand(IUserSession userSession, IExtraService extraService) : base(userSession)
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

            var extrasNames = string.Join(" ", parameters.Skip(1));

            if (string.IsNullOrEmpty(extrasNames))
            {
                throw new ArgumentException("Invalid extra/s!");
            }
            var extra = this.extraService.AddExtraToCar(id, extrasNames);
            return $"Added extra {extra.Name} to car with Id {id}";
        }
    }
}
