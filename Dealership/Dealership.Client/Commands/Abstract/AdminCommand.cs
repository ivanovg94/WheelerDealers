using System;
using System.Collections.Generic;
using System.Text;
using Dealership.Data.Models;
using Dealership.Data.Models.Contracts;

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
