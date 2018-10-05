using Dealership.Client.Core.Abstract;
using Dealership.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Client.Commands
{
    public class RemoveCommand : PrimeCommand
    {
        public RemoveCommand(IDealershipContext context) : base(context)
        {

        }

        public override string ProcessCommand(string[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
