using Dealership.Client.Commands.Abstract;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using System;

namespace Dealership.Client.Commands.ExtrasCommands
{
    public class CreateExtraCommand : AdminCommand
    {
        public CreateExtraCommand(IUserSession userSession) : base(userSession)
        {
        }

        public IExtraService ExtraService { get; set; }

        //createExtra | name
        public override string Execute(string[] parameters)
        {
            base.Execute(parameters);
            if (parameters.Length == 0) { throw new ArgumentException("Invalid parameters"); }
            var extra = this.ExtraService.CreateExtra(parameters[0]);
            return $"Created extra {extra.Name} with Id {extra.Id}";
        }
    }
}
