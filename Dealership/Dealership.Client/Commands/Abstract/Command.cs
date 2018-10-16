using Autofac;
using Dealership.Client.Contracts.Abstract;
using Dealership.Data.Context;
using Dealership.Data.Models.Contracts;
using System;

namespace Dealership.Client.Commands.Abstract
{
    public abstract class Command : ICommand
    {
        public IDealershipContext Context { get; set; }

        public IComponentContext AutoFacContext { get; set; }

        public IUserSession UserSession { get; set; }

        public Command(IUserSession userSession)
        {
            if (userSession == null)
            {
                throw new ArgumentNullException("UserSession cannot be null!");
            }
            this.UserSession = userSession;
        }

        public abstract string Execute(string[] parameters);
    }
}
