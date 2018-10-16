using Dealership.Client.Commands.Abstract;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using System;

namespace Dealership.Client.Commands.UserCommands
{
    public class LoginCommand : Command
    {
        private readonly IUserService userService;

        public LoginCommand(IUserSession userSession, IUserService userService) : base(userSession)
        {
            this.userService = userService;
        }

        public override string Execute(string[] parameters)
        {
            if (parameters.Length != 2)
            {
                throw new ArgumentException("Invalid parameters");
            }

            string username = parameters[0];
            string password = parameters[1];

            if (base.UserSession.CurrentUser != null)
            {
                throw new InvalidOperationException("There is logged in user.");
            }

            var user = this.userService.GetUserByCredentials(username, password);
            base.UserSession.CurrentUser = user;

            return $"User {username} successfully logged in!";
        }
    }
}
