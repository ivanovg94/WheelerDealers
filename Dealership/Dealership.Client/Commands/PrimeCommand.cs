using Dealership.Client.Contracts;
using Dealership.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Client.Commands
{
    public abstract class PrimeCommand : ICommand
    {
        public IDealershipContext Context { get; set; }
        public PrimeCommand()
        {
        }

        public abstract string Execute(string[] parameters);

    }
}
