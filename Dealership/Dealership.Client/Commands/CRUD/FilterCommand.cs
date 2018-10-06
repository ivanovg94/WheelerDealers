using Dealership.Client.Commands.Abstract;
using Dealership.Client.Core.Abstract;
using Dealership.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Client.Commands.CRUD
{
    public class FilterCommand : PrimeCommand
    {
        public FilterCommand()
        {
        }

        public override string Execute(string[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
