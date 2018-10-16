using Dealership.Data.Models;
using Dealership.Data.Models.Contracts;
using System;

namespace Dealership.Client.Commands.Abstract
{
    public abstract class AdminCommand : Command
    {
        public AdminCommand(IUserSession userSession) : base(userSession)
        {
        }

        public override string Execute(string[] parameters)
        {
            if (UserSession.CurrentUser.UserType != UserType.Admin)
            {
                throw new ArgumentException("Permission denied. You should be admin to do this operation.");
            }
            return "";
        }
    }
}
