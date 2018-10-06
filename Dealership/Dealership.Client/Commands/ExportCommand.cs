using Dealership.Client.Commands.Abstract;
using Dealership.Client.Core.Abstract;
using Dealership.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Client.Commands
{
    public class ExportCommand : PrimeCommand
    {
        public ExportCommand()
        {
        }

        public override string Execute(string[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
