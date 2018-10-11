using Autofac;
using Dealership.Client.Commands.Abstract;
using Dealership.Client.Contracts.Abstract;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using System;
using System.Linq;
using System.Reflection;

namespace Dealership.Client.Commands.CRUD
{
    public class EditCommand : PrimeCommand
    {
        public ICarService CarService { get; set; }

        // edit [exact property.Name] [id] [newValue] 'optional => [secondNewValue]'
        public override string Execute(string[] parameters)
        {
            var prop = parameters[0];
            var id = parameters[1];

            //string secondNewValue = "";

            //if (parameters.Count() == 4)
            //{
            //    secondNewValue = parameters[3];
            //}

            var methods = this.CarService.GetType().GetMethods();

            foreach (var method in methods)
            {
                if (method.Name.Contains("Edit" + prop))
                {
                    method.Invoke(CarService, new object[] { parameters.Skip(1).ToArray() });
                    break;
                }
            }

            return $"{prop} of car with id:{id} edited successfully!";
        }
    }
}
