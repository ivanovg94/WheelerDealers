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


        // edit [id]
        public override string Execute(string[] parameters)
        {


            var id = int.Parse(parameters[0]);
            var prop = parameters[1];
            var newValue = parameters[2];
            //get CarService public methods
            var methods = this.CarService.GetType().GetMethods();

            foreach (var method in methods)
            {// find adequate method and
                if (method.Name.Contains("Edit" + prop))
                {
                    method.Invoke(CarService, new object[] { id, newValue });
                    break;
                }
            }

            return "EditBrandCommand reached!"; // for testing purposes











            //var id = parameters[0];
            //var prop = parameters[1];
            //var newValue = parameters[2];
            //var commandName = "edit" + $"{prop.ToLower()}";
            //var func = base.autoFacContext.ResolveNamed<ICommand>(commandName);
            //func.Execute(parameters);






            //func.Execute(new string[] { newValue });
            //test
            //var methods = CarService.GetType().GetMethods();
            //foreach (var method in methods)
            //{
            //    if (method.Name.Contains(prop))
            //    {
            //        method.Invoke();
            //    }
            //}
            //test end
            //works for brand for now
            //int carId = int.Parse(parameters[0]);
            //var prop = parameters[1];
            //var newValue = parameters[2];
            //var car = CarService.GetCar(carId);
            //foreach (var propi in car.GetType().GetProperties())
            //{
            //    if (propi.Name == prop)
            //    {
            //        propi.SetValue(car,new Brand() { Name = newValue});
            //        break;
            //    }
            //}
            return "EditCommand reached";


        }
    }
}
