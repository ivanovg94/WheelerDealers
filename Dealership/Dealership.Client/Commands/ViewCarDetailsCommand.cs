using System;
using System.Collections.Generic;
using System.Text;
using Dealership.Data.Context;

namespace Dealership.Client.Commands
{
    public class ViewCarDetailsCommand : PrimeCommand
    {
        public ViewCarDetailsCommand(IDealershipContext context) : base(context)
        {
        }

        public override string ProcessCommand(string[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
