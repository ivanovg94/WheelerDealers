using Dealership.Client.Commands.Abstract;
using Dealership.Services;
using Dealership.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Client.Commands.CRUD
{
    public class AddCarExtra : PrimeCommand
    {
        public ICarService CarService { get; set; }

        //carId, extras
        public override string Execute(string[] parameters)
        {
            throw new NotImplementedException();
         //   var existingExtras = 
        }
    }
}
