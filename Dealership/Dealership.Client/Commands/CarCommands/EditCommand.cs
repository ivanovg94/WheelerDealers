using Autofac;
using Dealership.Client.Commands.Abstract;
using Dealership.Client.Contracts.Abstract;
using Dealership.Data.Models;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using System;
using System.Linq;
using System.Reflection;

namespace Dealership.Client.Commands.CarCommands
{
    public class EditCommand : AdminCommand
    {
        private readonly IEditCarService editCarService;

        public EditCommand(IUserSession userSession, IEditCarService editCarService) : base(userSession)
        {
            if (editCarService == null)
            {
                throw new ArgumentNullException("EditCarService cannot be null!");
            }

            this.editCarService = editCarService;
        }

        public override string Execute(string[] parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("Invalid input parameters!");
            }
            base.Execute(parameters);

            var prop = parameters[0];
            var id = parameters[1];

            var methods = this.editCarService.GetType().GetMethods();

            object invocationResult = null;

            foreach (var method in methods)
            {
                if (method.Name.ToLower().Contains(prop.ToLower()))
                {
                    invocationResult = method.Invoke(editCarService, new object[] { parameters.Skip(1).ToArray() });
                    break;
                }
            }
            if (invocationResult == null)
            {
                return $"Editing {prop} of car with ID:{id} failed!";
            }
            return invocationResult.ToString();
        }
    }
}
