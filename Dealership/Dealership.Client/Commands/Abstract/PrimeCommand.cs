using Autofac;
using Dealership.Client.Contracts;
using Dealership.Client.Contracts.Abstract;
using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Data.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Client.Commands.Abstract
{
    public abstract class PrimeCommand : ICommand
    {
        //Remove after all command logic is moved to service layer
        public IDealershipContext Context { get; set; }

        public IComponentContext AutoFacContext { get; set; }

        public IUserSession UserSession { get; set; }

        public PrimeCommand(IUserSession userSession)
        {
            this.UserSession = userSession;
        }

        public abstract string Execute(string[] parameters);

    }
}
