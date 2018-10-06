using System;
using System.Collections.Generic;
using System.Text;
using Dealership.Client.Commands.Abstract;
using Dealership.Data.Context;

namespace Dealership.Client.Commands.CRUD
{
    public class ViewCarDetailsCommand : PrimeCommand
    {
        public ViewCarDetailsCommand()
        {
        }

        public override string Execute(string[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
