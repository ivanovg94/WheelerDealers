using Dealership.Client.Core.Abstract;
using Dealership.Data.Context;
using Dealership.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dealership.Client.Commands
{
    public class AddCarCommand : PrimeCommand
    {
        public AddCarCommand()
        {
        }


        //add brand, model, hp, engCap, prod.date,price, chasis, nDoors, colorName,ColorType,ColorCode, fuelType, gearbox, nGears
        public override string Execute(string[] parameters)
        {
            //var brand = parameters[0];
            //var model = parameters[0];
            //var prodDate = parameters[0];
            //var price = parameters[0];
            //var chasis = parameters[0];
            //var nDoors = parameters[0];
            //var colorName = parameters[0];
            //var colorType = parameters[0];
            //var colorCode = parameters[0];
            //var fuelType = parameters[0];
            //var gearbox = parameters[0];
            //var nNumberOfGears = parameters[0];

        var test=    base.Context.Brands.FirstOrDefault();


            return "";
         //   base.context.Cars.
        }
    }
}
