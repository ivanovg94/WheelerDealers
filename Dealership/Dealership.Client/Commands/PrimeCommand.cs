using Dealership.Client.Core.Abstract;
using Dealership.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Client.Commands
{
    public abstract class PrimeCommand : ICommand
    {
        private IDealershipContext dealershipContext;

        public PrimeCommand(IDealershipContext context)
        {
            this.dealershipContext = context;
        }

        protected IDealershipContext DealershipContext { get => dealershipContext; }

        public abstract string ProcessCommand(string[] parameters);
        
    }
}
