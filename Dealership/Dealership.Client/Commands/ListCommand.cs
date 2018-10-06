using Dealership.Client.Core.Abstract;
using Dealership.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Client.Commands
{
    public class ListCommand : PrimeCommand
    {
        public ListCommand()
        {
        }

        //list null/sold 
        public override string Execute(string[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
