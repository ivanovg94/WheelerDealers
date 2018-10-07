using Dealership.Client.Commands.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Client.Commands.CRUD.EditCommands
{
    public class EditBrandCommand : PrimeCommand
    {
        public override string Execute(string[] parameters)
        {
            throw new NotImplementedException();

            var id = parameters[0];
            var newBrand = parameters[1];

            //calls Dealership.Service.????.Edit(id,brand);
            
        }
    }
}
