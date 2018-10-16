using Dealership.Client.Commands.Abstract;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using System;

namespace Dealership.Client.Commands.ExtrasCommands
{
    public class CreateExtraCommand : AdminCommand
    {
        private readonly IExtraService extraService;

        public CreateExtraCommand(IUserSession userSession, IExtraService extraService) : base(userSession)
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
            if (parameters[0]==string.Empty)
            {
                throw new ArgumentException("Name cannot be an empty string!");

            }
            var extra = this.extraService.CreateExtra(parameters[0]);
            return $"Created extra {extra.Name} with Id {extra.Id}";
        }
    }
}
