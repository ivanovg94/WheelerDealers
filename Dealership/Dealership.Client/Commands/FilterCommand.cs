using Dealership.Client.Core.Abstract;
using Dealership.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Client.Commands
{
    public class FilterCommand : PrimeCommand
    {
        public FilterCommand(IDealershipContext context) : base(context)
        {

        }

        public override string ProcessCommand(string[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
