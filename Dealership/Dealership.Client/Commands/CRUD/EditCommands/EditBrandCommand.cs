using Dealership.Client.Commands.Abstract;
using Dealership.Data.Models;
using Dealership.Services;
using Dealership.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dealership.Client.Commands.CRUD.EditCommands
{
    public class EditBrandCommand : PrimeCommand
    {
        public ICarService CarService { get; set; }

        public override string Execute(string[] parameters)
        {
            //var id = int.Parse(parameters[0]);
            //var prop = parameters[1];
            //var newValue = parameters[2];

            //var methods = this.CarService.GetType().GetMethods();

            //foreach (var method in methods)
            //{
            //    if (method.Name.Contains("Edit" + prop))
            //    {
            //        var car = Context.Cars.First(c => c.Id == id);
            //        method.Invoke(CarService, new object[] { id, newValue });
            //    }
            //}

            return "EditBrandCommand reached!";
        }
    }
}
