using Dealership.Client.Commands.Abstract;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using System;

namespace Dealership.Client.Commands.UserCommands
{
    public class RegisterCommand : Command
    {
        private readonly IUserService userService;

        public RegisterCommand(IUserSession userSession, IUserService userService) : base(userSession)
        {
            this.userService = userService;
        }

        public override string Execute(string[] parameters)
        {
            if (parameters.Length != 4)
            {
                throw new ArgumentException("Invalid parameters!");
            }

            if (base.UserSession.CurrentUser != null)
            {
                throw new InvalidOperationException("There is logged in user. Logout first to register new user!");
            }

            string username = parameters[0];
            string password = parameters[1];
            string confirmPassword = parameters[2];
            string email = parameters[3];

            var user = this.userService.RegisterUser(username, password, confirmPassword, email);
            base.UserSession.CurrentUser = user;

            return $"User {username} was registered succesfully.";
        }
    }
}
