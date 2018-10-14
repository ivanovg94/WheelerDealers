using Dealership.Client.Commands.Abstract;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using System;

namespace Dealership.Client.Commands.UserCommands
{
    public class LogoutCommand : Command
    {
        public LogoutCommand(IUserSession userSession) : base(userSession)
        {
        }

        public IUserService UserService { get; set; }

        public override string Execute(string[] parameters)
        {
            string username = base.UserSession.CurrentUser?.Username;

            if (base.UserSession.CurrentUser == null)
            {
                throw new InvalidOperationException("There is no logged in user.");
            }

            base.UserSession.CurrentUser = null;

            return $"User {username} successfully logged out!";
        }
    }
}
