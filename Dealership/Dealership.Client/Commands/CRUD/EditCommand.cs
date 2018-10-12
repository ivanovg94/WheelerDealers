using Autofac;
using Dealership.Client.Commands.Abstract;
using Dealership.Client.Contracts.Abstract;
using Dealership.Data.Models;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using System;
using System.Linq;
using System.Reflection;

namespace Dealership.Client.Commands.CRUD
{
    public class EditCommand : PrimeCommand
    {
        public ICarService CarService { get; set; }
        public IEditCarService EditCarService { get; set; }

        public EditCommand(IUserSession userSession) : base(userSession)
        {
        }


        // edit [exact property.Name] [id] [newValue] 'if property has more than one values => [secondNewValue]'
        public override string Execute(string[] parameters)
        {
            var prop = parameters[0];
            var id = parameters[1];

            var methods = this.EditCarService.GetType().GetMethods();


            foreach (var method in methods)
            {
                if (method.Name.Contains("Edit" + prop))
                {
                    method.Invoke(EditCarService, new object[] { parameters.Skip(1).ToArray() });
                    break;
                }
            }

            return $"Edit command exit!"; // test purpose only
        }
    }
}
