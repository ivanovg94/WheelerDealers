using Dealership.Client.Core.Abstract;
using Dealership.Data.Context;
using Dealership.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dealership.Client.Commands
{
    public class AddCommand : PrimeCommand
    {
        public AddCommand(IDealershipContext context) : base(context)
        {

        }
        public override string ProcessCommand(string[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
